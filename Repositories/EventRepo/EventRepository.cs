using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketSystem.Repositories.EventRepo
{
    /// <summary>
    /// Concrete implementation of IEventRepository for managing Event entities.
    /// Utilizes EF Core DbContext for data persistence and retrieval.
    /// </summary>
    public class EventRepository : IEventRepository
    {
        private readonly ConcertTicketSystemDbContext _context;

        /// <summary>
        /// Constructor with dependency injection of DbContext.
        /// </summary>
        /// <param name="context">Database context instance</param>
        public EventRepository(ConcertTicketSystemDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds a new Event entity asynchronously.
        /// Persists changes immediately to the database.
        /// </summary>
        /// <param name="request">Event entity to add</param>
        /// <returns>Id of the newly created Event</returns>
        public async Task<Guid> AddAsync(Event request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await _context.Events.AddAsync(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }

        /// <summary>
        /// Retrieves all Event entities asynchronously.
        /// Note: Returns basic Event details without related entities.
        /// </summary>
        /// <returns>Enumerable collection of Events</returns>
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific Event by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">Unique identifier of the Event</param>
        /// <returns>Event entity or null if not found</returns>
        public async Task<Event> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Invalid Event Id", nameof(id));

            return await _context.Events.FindAsync(id);
        }

        /// <summary>
        /// Retrieves upcoming events including their associated venues and ticket details.
        /// Only includes events with start time in the future (UTC).
        /// Eager loading used for related entities to optimize query performance.
        /// </summary>
        /// <returns>Enumerable of upcoming Event entities with related data</returns>
        public async Task<IEnumerable<Event>> GetUpcomingEventsWithTicketsAsync()
        {
            return await _context.Events
                .AsNoTracking()
                .Include(e => e.Venue)
                .Include(e => e.TicketTypes)
                    .ThenInclude(tt => tt.Tickets)
                .Where(e => e.StartTime >= DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
