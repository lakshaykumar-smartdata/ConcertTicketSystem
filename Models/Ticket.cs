namespace ConcertTicketSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }

        public string Status { get; set; } = "Reserved"; // Reserved, Purchased, Cancelled
        public DateTime ReservedAt { get; set; }
        public DateTime? PurchasedAt { get; set; }
    }
}
