using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.EventRepo
{
    public interface IEventRepository
    {
        Task<Guid> AddAsync(Event venue);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(Guid id);
        Task<IEnumerable<Event>> GetUpcomingEventsWithTicketsAsync();
    }
}
