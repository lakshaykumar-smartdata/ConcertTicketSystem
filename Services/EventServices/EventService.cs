using ConcertTicketSystem.Dto.ResponseDto;
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
        public async Task<IEnumerable<EventViewModel>> GetAllEventsWithAvailabilityAsync()
        {
            var events = await _repository.GetUpcomingEventsWithTicketsAsync();

            return events.Select(e => new EventViewModel
            {
                Id = e.Id,
                Title = e.Name,
                EventDate = e.Date,
                EventStartTime = e.StartTime,
                EventEndTime = e.EndTime,
                VenueName = e.Venue.Name,
                TicketTypes = e.TicketTypes.Select(tt => new TicketTypeViewModel
                {
                    TicketTypeId = tt.Id,
                    Name = tt.Name,
                    Price = tt.Price,
                    TotalQuantity = tt.QuantityAvailable,
                    AvailableCount = tt.Tickets.Count(t =>
                        !t.IsPurchased &&
                        (!t.ReservationExpiresAt.HasValue || t.ReservationExpiresAt < DateTime.UtcNow))
                }).ToList()
            });
        }
    }
}
