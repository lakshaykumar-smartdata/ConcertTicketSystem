using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        public string ReservationCode { get; set; } = string.Empty;  // Unique reservation identifier
        public DateTime? ReservationExpiresAt { get; set; }
        public bool IsPurchased { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PurchasedAt { get; set; }
        public Guid TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        // --- Computed flags ---
        [NotMapped]
        public bool IsReserved =>
            !string.IsNullOrEmpty(ReservationCode) &&
            ReservationExpiresAt.HasValue &&
            ReservationExpiresAt > DateTime.UtcNow;

        [NotMapped]
        public bool IsAvailable =>
            !IsPurchased &&
            (!ReservationExpiresAt.HasValue || ReservationExpiresAt < DateTime.UtcNow);
    }
}
