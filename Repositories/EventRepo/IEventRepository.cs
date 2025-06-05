using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.EventRepo
{
    public interface IEventRepository
    {
        Task<Guid> AddAsync(Event venue);
        Task AddTicketAsync(List<Ticket> tickets);
        Task<Guid> AddTicketTypeAsync(TicketType request);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event> GetByIdAsync(Guid id);
    }
}
