using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.EventRepo
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event evnt);
        Task UpdateAsync(Event evnt);
        Task DeleteAsync(int id);
    }
}
