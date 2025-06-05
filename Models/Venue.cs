using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Models
{
    public class Venue
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }  // Max attendees allowed

        public ICollection<Event> Events { get; set; }
    }
}
