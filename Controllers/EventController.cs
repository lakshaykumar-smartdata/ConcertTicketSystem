using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.EventServices;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        /// <summary>
        /// Constructor for EventController. Injects the IEventService dependency.
        /// </summary>
        /// <param name="eventService">Event service interface</param>
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Endpoint to create a new event.
        /// Validates input and persists the event via service layer.
        /// </summary>
        /// <param name="request">AddEventDto containing event details</param>
        /// <returns>ActionResult with success status and new event ID</returns>
        [HttpPost("Create-Event")]
        public async Task<IActionResult> CreateEvent([FromBody] AddEventDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Invalid request data." });

            // Model validation handled by [ApiController] attribute, but double-check here if needed
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var eventEntity = new Event
            {
                Id = request.Id,
                Name = request.Name,
                Date = request.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Description = request.Description,
                VenueId = request.VenueId,
            };

            var eventId = await _eventService.AddAsync(eventEntity);

            return Ok(new { success = true, message = "Event created successfully.", eventId });
        }

        /// <summary>
        /// Retrieves all events.
        /// Returns 404 if no events found.
        /// </summary>
        /// <returns>List of events</returns>
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetAllAsync();

            if (events == null || !events.Any())
                return NotFound(new { success = false, message = "Events not found." });

            return Ok(new { success = true, data = events });
        }
    }
}
