using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Models
{
    public class TicketType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., Regular, VIP
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }  // Tickets available for this type

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
