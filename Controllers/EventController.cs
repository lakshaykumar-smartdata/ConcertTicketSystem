using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.EventServices;
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
        [HttpPost("Create-Ticket-Type")]
        public async Task<IActionResult> CreateTicketType([FromBody] AddTicketTypeDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Invalid request data." });
            TicketType ticketType = new()
            {
                Id = request.Id,
                Name = request.Name,
                EventId = request.EventId,
                QuantityAvailable = request.QuantityAvailable,
                Price = request.Price,
            };
            var ticketTypeId = await _eventService.AddTicketTypeAsync(ticketType);

            // Return venueId
            return Ok(new { success = true, message = "ticket Type created successfully.", ticketTypeId = ticketTypeId });
        }
    }
}
