namespace ConcertTicketSystem.Dto.ResponseDto
{
    /// <summary>
    /// Data Transfer Object representing ticket type details for client-facing responses.
    /// Provides pricing and availability information for a specific ticket category.
    /// </summary>
    public class TicketTypeViewModel
    {
        /// <summary>
        /// Unique identifier of the ticket type.
        /// </summary>
        public Guid TicketTypeId { get; set; }

        /// <summary>
        /// Name or label of the ticket type (e.g., Regular, VIP).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Price for a single ticket of this type.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Total quantity of tickets initially available for this ticket type.
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Number of tickets currently available for sale or reservation.
        /// Excludes tickets that are already purchased or reserved.
        /// </summary>
        public int AvailableCount { get; set; }
    }
}
