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
        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            // Step 1: Save the ticket type
            var ticketTypeId = await _repository.AddTicketTypeAsync(request);
            // Step 2: Generate physical tickets based on quantity
            var tickets = new List<Ticket>();
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
            await _repository.AddTicketAsync(tickets);
            return ticketTypeId;
        }
    }
}
