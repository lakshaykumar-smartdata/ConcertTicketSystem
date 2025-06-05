using System;
using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Dto.RequestDto
{
    /// <summary>
    /// DTO for creating or updating an event.
    /// Encapsulates all necessary fields to define event metadata.
    /// </summary>
    public class AddEventDto
    {
        /// <summary>
        /// Optional unique identifier for the event.
        /// Typically set by the system for updates; can be omitted on creation.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name or title of the event.
        /// Required and limited to 150 characters for database consistency.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Date on which the event is scheduled.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Event start time on the specified date.
        /// </summary>
        [Required]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Event end time on the specified date.
        /// Should be greater than StartTime (validation to be handled in service layer).
        /// </summary>
        [Required]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Detailed description of the event.
        /// Maximum length capped to maintain database consistency.
        /// </summary>
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Identifier of the Venue where the event will take place.
        /// Required to establish event location context.
        /// </summary>
        [Required]
        public Guid VenueId { get; set; }
    }
}
