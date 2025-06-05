using System.ComponentModel.DataAnnotations;

namespace ConcertTicketSystem.Dto.RequestDto
{
    public class CancelReservationRequest
    {
        /// <summary>
        /// Reservation code associated with the ticket.
        /// Used for verifying the reservation before cancel.
        /// Required for authorization of the cancel operation.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string ReservationCode { get; set; } = string.Empty;
    }
}
