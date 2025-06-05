using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.EventRepo;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace ConcertTicketSystem.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repo)
        {
            _repository = repo;
        }
        public async Task<Guid> AddAsync(Event request)
        {
            return await _repository.AddAsync(request);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
