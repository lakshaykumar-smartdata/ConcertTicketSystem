using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.TicketRepo;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketSystem.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        public readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            // Step 1: Save the ticket type
            var ticketTypeId = await _ticketRepository.AddTicketTypeAsync(request);
            // Step 2: Generate physical tickets based on quantity
            var tickets = new List<Ticket>();
            for (int i = 0; i < request.QuantityAvailable; i++)
            {
                tickets.Add(new Ticket
                {
                    Id = Guid.NewGuid(),
                    TicketTypeId = ticketTypeId,
                    IsPurchased = false,
                    ReservationCode = string.Empty,
                    ReservationExpiresAt = null
                });
            }
            await _ticketRepository.AddTicketAsync(tickets);
            return ticketTypeId;
        }
        public async Task<Ticket?> ReserveTicketAsync(Guid ticketTypeId, TimeSpan holdDuration)
        {
            var ticket = await _ticketRepository.GetAvailableTicket(ticketTypeId);

            if (ticket == null) return null;

            ticket.ReservationCode = Guid.NewGuid().ToString();
            ticket.ReservationExpiresAt = DateTime.UtcNow.Add(holdDuration);

            await _ticketRepository.UpdateTicketAsync(ticket);
            return ticket;
        }
        public async Task<bool> PurchaseAsync(Guid ticketId, string reservationCode)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);

            if (ticket == null ||
                ticket.IsPurchased ||
                ticket.ReservationCode != reservationCode ||
                ticket.ReservationExpiresAt < DateTime.UtcNow)
                return false;

            ticket.IsPurchased = true;
            ticket.ReservationCode = null;
            ticket.ReservationExpiresAt = null;

            await _ticketRepository.UpdateTicketAsync(ticket);
            return true;
        }


    }
}
