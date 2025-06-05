using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketSystem.Models
{
    /// <summary>
    /// Represents an individual ticket instance, linked to a specific ticket type.
    /// Tracks reservation, purchase status, and availability.
    /// </summary>
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Primary key with auto-generated GUID
        public Guid Id { get; set; }

        [MaxLength(150)]
        public string ReservationCode { get; set; } = string.Empty;  // Unique alphanumeric code used for temporary holds or reservations

        public DateTime? ReservationExpiresAt { get; set; } // UTC timestamp when the reservation hold expires

        public bool IsPurchased { get; set; } // Indicates if the ticket has been finalized for purchase

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the ticket record was created

        public DateTime? PurchasedAt { get; set; } // Timestamp for when the ticket was purchased (null if not purchased)

        [Required]
        public Guid TicketTypeId { get; set; } // Foreign key to associated TicketType

        [ForeignKey(nameof(TicketTypeId))]
        public TicketType TicketType { get; set; } = null!; // Navigation property to the TicketType entity


        // --- Computed properties (NotMapped) ---

        /// <summary>
        /// Indicates whether the ticket is currently reserved but not yet purchased.
        /// </summary>
        [NotMapped]
        public bool IsReserved =>
            !string.IsNullOrEmpty(ReservationCode) &&
            ReservationExpiresAt.HasValue &&
            ReservationExpiresAt > DateTime.UtcNow;

        /// <summary>
        /// Returns true if the ticket is not purchased and either not reserved or the reservation expired.
        /// </summary>
        [NotMapped]
        public bool IsAvailable =>
            !IsPurchased &&
            (!ReservationExpiresAt.HasValue || ReservationExpiresAt < DateTime.UtcNow);
    }
}
