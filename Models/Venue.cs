using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketSystem.Models
{
    /// <summary>
    /// Represents a physical location where events are hosted.
    /// Includes capacity and location metadata.
    /// </summary>
    public class Venue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Primary key - auto-generated GUID
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty; // Venue name (e.g., Madison Square Garden)

        [Required]
        [MaxLength(250)]
        public string Location { get; set; } = string.Empty; // Full address or location descriptor

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        public int Capacity { get; set; } // Maximum number of attendees supported by venue

        /// <summary>
        /// Events scheduled at this venue.
        /// </summary>
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
