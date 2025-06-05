using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Dto.RequestDto
{
    /// <summary>
    /// DTO representing a purchase request for a ticket.
    /// Contains necessary identifiers to validate and process ticket purchase.
    /// </summary>
    public class PurchaseRequest
    {
        /// <summary>
        /// Unique identifier of the ticket to be purchased.
        /// </summary>
        [Required]
        public Guid TicketId { get; set; }

        /// <summary>
        /// Reservation code associated with the ticket.
        /// Used for verifying the reservation before purchase.
        /// Required for authorization of the purchase operation.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string ReservationCode { get; set; } = string.Empty;
    }
}
