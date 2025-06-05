using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Dto.RequestDto
{
    /// <summary>
    /// DTO to encapsulate data required to create or update a ticket type.
    /// Defines ticket category, pricing, and availability within an event context.
    /// </summary>
    public class AddTicketTypeDto
    {
        /// <summary>
        /// Optional unique identifier for the ticket type.
        /// Used primarily during updates; omit or empty for new creations.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the ticket type, e.g., Regular, VIP.
        /// Required with a sensible max length to enforce database constraints.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Price per ticket for this ticket type.
        /// Must be a positive decimal value.
        /// </summary>
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Number of tickets available for sale for this ticket type.
        /// Must be a non-negative integer.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "QuantityAvailable cannot be negative.")]
        public int QuantityAvailable { get; set; }

        /// <summary>
        /// Foreign key identifying the event to which this ticket type belongs.
        /// Required to link ticket type to its event.
        /// </summary>
        [Required]
        public Guid EventId { get; set; }
    }
}
