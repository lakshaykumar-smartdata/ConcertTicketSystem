using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.EventServices;
using ConcertTicketSystem.Services.VenueServices;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _service;

        public VenueController(IVenueService service)
        {
            _service = service;
        }
        [HttpPost("Create-Venue")]
        public async Task<IActionResult> CreateVenue([FromBody] AddVenueDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Invalid request data." });
            Venue venue = new()
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location,
                Capacity = request.Capacity,
            };
            var venueId = await _service.AddAsync(venue);

            // Return venueId
            return Ok(new { success = true, message = "venue created successfully.", venueId = venueId });
        }
    }
}
