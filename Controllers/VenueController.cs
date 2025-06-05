using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.VenueServices;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService _service;

        /// <summary>
        /// Injects VenueService dependency.
        /// </summary>
        /// <param name="service">Venue service interface</param>
        public VenueController(IVenueService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new venue entity.
        /// </summary>
        /// <param name="request">Venue creation request DTO</param>
        /// <returns>Action result with venue identifier and operation status</returns>
        [HttpPost("Create-Venue")]
        public async Task<IActionResult> CreateVenue([FromBody] AddVenueDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Invalid request data." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var venue = new Venue
            {
                Id = request.Id,
                Name = request.Name,
                Location = request.Location,
                Capacity = request.Capacity,
            };

            var venueId = await _service.AddAsync(venue);

            return Ok(new { success = true, message = "Venue created successfully.", venueId });
        }
    }
}
