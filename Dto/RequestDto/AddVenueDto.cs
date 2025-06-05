using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Dto.RequestDto
{
    /// <summary>
    /// DTO for creating or updating a venue.
    /// Captures essential venue details including name, location, and capacity.
    /// </summary>
    public class AddVenueDto
    {
        /// <summary>
        /// Optional unique identifier for the venue.
        /// Used primarily for updates; can be omitted during creation.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the venue.
        /// Required, with max length aligned to database constraints.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Physical location or address of the venue.
        /// Required, with max length to maintain consistency.
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Maximum capacity of attendees the venue can accommodate.
        /// Must be a positive integer.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
        public int Capacity { get; set; }
    }
}
