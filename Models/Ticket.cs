using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        public string ReservationCode { get; set; } = string.Empty;  // Unique reservation identifier
        public DateTime? ReservationExpiresAt { get; set; }
        public bool IsReserved => ReservationExpiresAt != null && ReservationExpiresAt > DateTime.UtcNow;
        public bool IsPurchased { get; set; }

        public Guid TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
    }
}
