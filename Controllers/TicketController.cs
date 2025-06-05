using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.TicketServices;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
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
            var ticketTypeId = await _ticketService.AddTicketTypeAsync(ticketType);

            // Return venueId
            return Ok(new { success = true, message = "ticket Type created successfully.", ticketTypeId = ticketTypeId });
        }
        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve([FromQuery] Guid ticketTypeId)
        {
            var reserved = await _ticketService.ReserveTicketAsync(ticketTypeId, TimeSpan.FromMinutes(10));

            if (reserved == null)
                return NotFound("No available tickets to reserve");

            return Ok(new
            {
                reserved.Id,
                reserved.ReservationCode,
                reserved.ReservationExpiresAt
            });
        }
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseRequest request)
        {
            if (request == null || request.TicketId == Guid.Empty || string.IsNullOrWhiteSpace(request.ReservationCode))
                return BadRequest("Invalid purchase request.");

            var success = await _ticketService.PurchaseAsync(request.TicketId, request.ReservationCode);

            if (!success)
                return BadRequest("Purchase failed. The ticket may be expired, already purchased, or invalid reservation code.");

            return Ok("Ticket purchased successfully.");
        }

    }
}
