namespace ConcertTicketSystem.Dto.ResponseDto
{
    /// <summary>
    /// Data Transfer Object representing event details for client-facing responses.
    /// Aggregates event metadata along with associated ticket types and venue information.
    /// </summary>
    public class EventViewModel
    {
        /// <summary>
        /// Unique identifier of the event.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Display title or name of the event.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Date when the event occurs.
        /// </summary>
        public DateTime EventDate { get; set; }

        /// <summary>
        /// Scheduled start time of the event.
        /// </summary>
        public DateTime EventStartTime { get; set; }

        /// <summary>
        /// Scheduled end time of the event.
        /// </summary>
        public DateTime EventEndTime { get; set; }

        /// <summary>
        /// Name of the venue where the event will take place.
        /// </summary>
        public string VenueName { get; set; } = string.Empty;

        /// <summary>
        /// Collection of ticket types available for this event.
        /// </summary>
        public List<TicketTypeViewModel> TicketTypes { get; set; } = new();
    }
}
