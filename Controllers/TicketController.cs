using ConcertTicketSystem.Dto.RequestDto;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Services.TicketServices;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTicketSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        /// <summary>
        /// Constructor injecting the ticket service dependency.
        /// </summary>
        /// <param name="ticketService">Ticket service interface</param>
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Creates a new ticket type linked to an event.
        /// </summary>
        /// <param name="request">Ticket type details</param>
        /// <returns>Created ticket type identifier</returns>
        [HttpPost("Create-Ticket-Type")]
        public async Task<IActionResult> CreateTicketType([FromBody] AddTicketTypeDto request)
        {
            if (request == null)
                return BadRequest(new { success = false, message = "Invalid request data." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketType = new TicketType
            {
                Id = request.Id,
                Name = request.Name,
                EventId = request.EventId,
                QuantityAvailable = request.QuantityAvailable,
                Price = request.Price,
            };

            var ticketTypeId = await _ticketService.AddTicketTypeAsync(ticketType);

            return Ok(new { success = true, message = "Ticket type created successfully.", ticketTypeId });
        }

        /// <summary>
        /// Reserves a ticket of the specified ticket type for a limited time.
        /// </summary>
        /// <param name="ticketTypeId">Identifier of the ticket type to reserve</param>
        /// <returns>Reservation details or not found response</returns>
        [HttpPost("reserve")]
        public async Task<IActionResult> Reserve([FromQuery] Guid ticketTypeId)
        {
            if (ticketTypeId == Guid.Empty)
                return BadRequest(new { success = false, message = "Invalid ticketTypeId provided." });

            var reservedTicket = await _ticketService.ReserveTicketAsync(ticketTypeId, TimeSpan.FromMinutes(10));

            if (reservedTicket == null)
                return NotFound(new { success = false, message = "No available tickets to reserve." });

            return Ok(new
            {
                success = true,
                reservedTicket.Id,
                reservedTicket.ReservationCode,
                reservedTicket.ReservationExpiresAt
            });
        }

        /// <summary>
        /// Purchases a reserved ticket, validating the reservation code.
        /// </summary>
        /// <param name="request">Purchase request containing ticket ID and reservation code</param>
        /// <returns>Success or failure response</returns>
        [HttpPost("purchase")]
        public async Task<IActionResult> PurchaseTicket([FromBody] PurchaseRequest request)
        {
            if (request == null || request.TicketId == Guid.Empty || string.IsNullOrWhiteSpace(request.ReservationCode))
                return BadRequest(new { success = false, message = "Invalid purchase request." });

            var purchaseResult = await _ticketService.PurchaseAsync(request.TicketId, request.ReservationCode);

            if (!purchaseResult)
                return BadRequest(new
                {
                    success = false,
                    message = "Purchase failed. The ticket may be expired, already purchased, or the reservation code is invalid."
                });

            return Ok(new { success = true, message = "Ticket purchased successfully." });
        }
        /// <summary>
        /// Cancels an existing ticket reservation identified by the reservation code.
        /// </summary>
        /// <param name="request">The cancellation request containing the reservation code.</param>
        /// <returns>
        /// Returns <see cref="BadRequestObjectResult"/> if the reservation code is missing or invalid.
        /// Returns <see cref="NotFoundObjectResult"/> if no active reservation matches the provided code.
        /// Returns <see cref="OkObjectResult"/> when the reservation is successfully cancelled.
        /// </returns>
        [HttpPost("cancel-reservation")]
        public async Task<IActionResult> CancelReservation([FromBody] CancelReservationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ReservationCode))
                return BadRequest("Reservation code is required.");

            var result = await _ticketService.CancelReservationAsync(request.ReservationCode);

            if (!result)
                return NotFound("Reservation not found or already expired.");

            return Ok("Reservation cancelled successfully.");
        }

    }
}
