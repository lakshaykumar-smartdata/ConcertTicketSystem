using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.EventServices;
using ConcertTicketSystem.Services.TicketServices;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpPost("Create-Event")]
        public async Task<IActionResult> CreateEvent([FromBody] AddEventDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Invalid request data." });
            Event eventRequest = new()
            {
                Id = request.Id,
                Name = request.Name,
                Date = request.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Description = request.Description,
                VenueId = request.VenueId,
            };
            var eventId = await _eventService.AddAsync(eventRequest);

            // Return venueId
            return Ok(new { success = true, message = "event created successfully.", eventId = eventId });
        }
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var success = await _eventService.GetAllAsync();
            if (!success.Any()) return NotFound("Events not found.");

            return Ok(success);
        }
    }
}
