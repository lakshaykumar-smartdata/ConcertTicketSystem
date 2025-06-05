namespace ConcertTicketSystem.Dto.ResponseDto
{
    public class TicketTypeViewModel
    {
        public Guid TicketTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableCount { get; set; } // Only unpurchased & unreserved
    }
}
