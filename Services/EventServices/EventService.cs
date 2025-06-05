using ConcertTicketSystem.Dto.ResponseDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.EventRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertTicketSystem.Services.EventServices
{
    /// <summary>
    /// Provides application-level business logic for Event-related operations.
    /// Orchestrates interaction between repository and DTO transformation.
    /// </summary>
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventService"/> class.
        /// </summary>
        /// <param name="repository">Event repository instance injected via DI.</param>
        /// <exception cref="ArgumentNullException">Thrown if repository is null.</exception>
        public EventService(IEventRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <inheritdoc/>
        public async Task<Guid> AddAsync(Event request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await _repository.AddAsync(request);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        /// <inheritdoc/>
        public async Task<Event> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid event Id.", nameof(id));

            return await _repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Retrieves upcoming events along with associated ticket availability details.
        /// Transforms domain models into response DTOs optimized for client consumption.
        /// </summary>
        /// <returns>A collection of <see cref="EventViewModel"/> with ticket availability.</returns>
        public async Task<IEnumerable<EventViewModel>> GetAllEventsWithAvailabilityAsync()
        {
            var events = await _repository.GetUpcomingEventsWithTicketsAsync();

            // Defensive null check
            if (events == null)
                return Enumerable.Empty<EventViewModel>();

            return events.Select(e => new EventViewModel
            {
                Id = e.Id,
                Title = e.Name,
                EventDate = e.Date,
                EventStartTime = e.StartTime,
                EventEndTime = e.EndTime,
                VenueName = e.Venue?.Name ?? string.Empty,
                TicketTypes = e.TicketTypes?.Select(tt => new TicketTypeViewModel
                {
                    TicketTypeId = tt.Id,
                    Name = tt.Name,
                    Price = tt.Price,
                    TotalQuantity = tt.QuantityAvailable,
                    AvailableCount = tt.Tickets.Count(t =>
                        !t.IsPurchased &&
                        (!t.ReservationExpiresAt.HasValue || t.ReservationExpiresAt < DateTime.UtcNow))
                }).ToList() ?? new List<TicketTypeViewModel>()
            });
        }
    }
}
