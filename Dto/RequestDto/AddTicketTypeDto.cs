namespace ConcertTicketSystem.Dto.RequestDto
{
    public class AddTicketTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } // e.g., Regular, VIP
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }  // Tickets available for this type

        public Guid EventId { get; set; }
    }
}
