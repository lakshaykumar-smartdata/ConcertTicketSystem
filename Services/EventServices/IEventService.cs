using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Services.EventServices
{
    public interface IEventService
    {
        Task<Guid> AddAsync(Event venue);
        Task<Guid> AddTicketTypeAsync(TicketType request);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(Guid id);
    }
}
