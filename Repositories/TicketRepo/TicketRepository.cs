using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketSystem.Repositories.TicketRepo
{
    /// <summary>
    /// Concrete implementation of ITicketRepository for managing Ticket and TicketType entities.
    /// Utilizes EF Core DbContext for transactional data persistence.
    /// </summary>
    public class TicketRepository : ITicketRepository
    {
        private readonly ConcertTicketSystemDbContext _context;

        /// <summary>
        /// Constructor injecting the database context dependency.
        /// </summary>
        /// <param name="context">Database context instance</param>
        public TicketRepository(ConcertTicketSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a new TicketType asynchronously and persists changes.
        /// </summary>
        /// <param name="request">TicketType entity to add</param>
        /// <returns>Id of the created TicketType</returns>
        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await _context.TicketTypes.AddAsync(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }

        /// <summary>
        /// Adds a batch of Ticket entities asynchronously.
        /// </summary>
        /// <param name="tickets">List of Ticket entities to add</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public async Task AddTicketAsync(List<Ticket> tickets)
        {
            if (tickets == null || tickets.Count == 0)
                throw new ArgumentException("Ticket list cannot be null or empty.", nameof(tickets));

            await _context.Tickets.AddRangeAsync(tickets);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves the first available Ticket for a given TicketType.
        /// A ticket is considered available if it is not purchased and either has no reservation or the reservation has expired.
        /// </summary>
        /// <param name="ticketTypeId">Id of the TicketType</param>
        /// <returns>An available Ticket entity or null if none found</returns>
        public async Task<Ticket> GetAvailableTicket(Guid ticketTypeId)
        {
            if (ticketTypeId == Guid.Empty)
                throw new ArgumentException("Invalid TicketTypeId.", nameof(ticketTypeId));

            return await _context.Tickets
                .AsNoTracking()
                .Where(t => t.TicketTypeId == ticketTypeId &&
                            !t.IsPurchased &&
                            (t.ReservationExpiresAt == null || t.ReservationExpiresAt < DateTime.UtcNow))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Updates an existing Ticket entity.
        /// Assumes that the entity is tracked or attaches it to the context before updating.
        /// </summary>
        /// <param name="ticket">Ticket entity to update</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            if (ticket == null) throw new ArgumentNullException(nameof(ticket));

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a Ticket by its unique identifier asynchronously.
        /// </summary>
        /// <param name="ticketId">Unique identifier of the Ticket</param>
        /// <returns>Ticket entity or null if not found</returns>
        public async Task<Ticket> GetTicketByIdAsync(Guid ticketId)
        {
            if (ticketId == Guid.Empty)
                throw new ArgumentException("Invalid ticketId.", nameof(ticketId));

            return await _context.Tickets.FindAsync(ticketId);
        }
        /// <summary>
        /// Retrieves a reserved ticket matching the specified reservation code.
        /// </summary>
        /// <param name="reservationCode">The unique reservation code to search for.</param>
        /// <returns>
        /// Returns the <see cref="Ticket"/> if found and currently reserved; otherwise, returns <c>null</c>.
        /// </returns>
        public async Task<Ticket?> GetTicketByReservationCodeAsync(string reservationCode)
        {
            return await _context.Tickets
                .FirstOrDefaultAsync(t => t.ReservationCode == reservationCode && !t.IsPurchased);
        }
    }
}
