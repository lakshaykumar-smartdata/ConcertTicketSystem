using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.TicketRepo;

namespace ConcertTicketSystem.Services.TicketServices
{
    /// <summary>
    /// Implements ticket-related business logic, including ticket type creation, reservation, and purchase workflows.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="TicketService"/>.
        /// </summary>
        /// <param name="ticketRepository">Injected ticket repository.</param>
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        /// <inheritdoc/>
        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request.QuantityAvailable <= 0)
                throw new ArgumentException("QuantityAvailable must be greater than zero.", nameof(request.QuantityAvailable));

            // Step 1: Persist ticket type metadata
            var ticketTypeId = await _ticketRepository.AddTicketTypeAsync(request);

            // Step 2: Generate physical ticket entities corresponding to quantity
            var tickets = new List<Ticket>(request.QuantityAvailable);
            for (int i = 0; i < request.QuantityAvailable; i++)
            {
                tickets.Add(new Ticket
                {
                    Id = Guid.NewGuid(),
                    TicketTypeId = ticketTypeId,
                    IsPurchased = false,
                    ReservationCode = null,
                    ReservationExpiresAt = null
                });
            }

            await _ticketRepository.AddTicketAsync(tickets);

            return ticketTypeId;
        }

        /// <inheritdoc/>
        public async Task<Ticket?> ReserveTicketAsync(Guid ticketTypeId, TimeSpan holdDuration)
        {
            if (ticketTypeId == Guid.Empty)
                throw new ArgumentException("Invalid ticketTypeId.", nameof(ticketTypeId));
            if (holdDuration <= TimeSpan.Zero)
                throw new ArgumentException("Hold duration must be positive.", nameof(holdDuration));

            // Find an available ticket for the specified ticket type
            var ticket = await _ticketRepository.GetAvailableTicket(ticketTypeId);
            if (ticket == null)
                return null;

            // Assign a unique reservation code and expiration timestamp
            ticket.ReservationCode = Guid.NewGuid().ToString("N");
            ticket.ReservationExpiresAt = DateTime.UtcNow.Add(holdDuration);

            await _ticketRepository.UpdateTicketAsync(ticket);
            return ticket;
        }

        /// <inheritdoc/>
        public async Task<bool> PurchaseAsync(Guid ticketId, string reservationCode)
        {
            if (ticketId == Guid.Empty)
                throw new ArgumentException("Invalid ticketId.", nameof(ticketId));
            if (string.IsNullOrWhiteSpace(reservationCode))
                throw new ArgumentException("Reservation code must be provided.", nameof(reservationCode));

            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);

            if (ticket == null)
                return false;

            // Validate purchase conditions
            if (ticket.IsPurchased ||
                ticket.ReservationCode != reservationCode ||
                !ticket.ReservationExpiresAt.HasValue ||
                ticket.ReservationExpiresAt < DateTime.UtcNow)
            {
                return false;
            }

            // Mark the ticket as purchased and clear reservation metadata
            ticket.IsPurchased = true;
            ticket.ReservationCode = null;
            ticket.ReservationExpiresAt = null;

            await _ticketRepository.UpdateTicketAsync(ticket);
            return true;
        }
        /// <inheritdoc/>
        public async Task<bool> CancelReservationAsync(string reservationCode)
        {
            if (string.IsNullOrWhiteSpace(reservationCode))
                throw new ArgumentException("Reservation code must be provided.", nameof(reservationCode));

            var ticket = await _ticketRepository.GetTicketByReservationCodeAsync(reservationCode);

            if (ticket == null)
                return false; // No matching active reservation found

            // Reset reservation details to release the hold
            ticket.ReservationCode = string.Empty;
            ticket.ReservationExpiresAt = null;

            await _ticketRepository.UpdateTicketAsync(ticket);
            return true;
        }
    }
}
