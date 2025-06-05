using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Dto.RequestDto
{
    public class AddEventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid VenueId { get; set; }
    }
}
