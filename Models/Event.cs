using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketSystem.Models
{
    /// <summary>
    /// Represents a scheduled concert or show linked to a specific venue.
    /// Contains event metadata and ticket type configuration.
    /// </summary>
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Primary key - auto-generated GUID
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty; // Name/title of the event

        [Required]
        public DateTime Date { get; set; } // Calendar date of the event (local to venue)

        [Required]
        public DateTime StartTime { get; set; } // Start time of the event

        [Required]
        public DateTime EndTime { get; set; } // End time of the event

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty; // Extended description, max 1000 chars

        [Required]
        public Guid VenueId { get; set; } // Foreign key to Venue

        [ForeignKey(nameof(VenueId))]
        public Venue Venue { get; set; } = null!; // Navigation property to Venue

        /// <summary>
        /// Collection of available ticket types for this event (e.g., VIP, General).
        /// </summary>
        public ICollection<TicketType> TicketTypes { get; set; } = new List<TicketType>();
    }
}
