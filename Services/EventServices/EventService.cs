using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.EventRepo;

namespace ConcertTicketSystem.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;

        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Event>> GetEventsAsync() => _repo.GetAllAsync();
        public Task<Event?> GetEventAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddEventAsync(Event evnt) => _repo.AddAsync(evnt);
        public Task UpdateEventAsync(Event evnt) => _repo.UpdateAsync(evnt);
        public Task DeleteEventAsync(int id) => _repo.DeleteAsync(id);

    }
}
