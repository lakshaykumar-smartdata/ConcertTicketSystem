using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketSystem.Models
{
    /// <summary>
    /// Defines a category of ticket (e.g., Regular, VIP) for a specific event.
    /// Includes pricing and quantity metadata.
    /// </summary>
    public class TicketType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Primary key - auto-generated GUID
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Label for ticket type (e.g., Regular, VIP)

        [Required]
        [Range(0, 100000)]
        public decimal Price { get; set; } // Cost per ticket (currency assumed in business logic)

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityAvailable { get; set; } // Maximum number of tickets available for this type

        [Required]
        public Guid EventId { get; set; } // Foreign key to Event

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; } = null!; // Navigation property to associated Event

        /// <summary>
        /// Collection of physical/generated tickets issued under this ticket type.
        /// </summary>
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
