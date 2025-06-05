namespace ConcertTicketSystem.Dto.RequestDto
{
    public class PurchaseRequest
    {
        public Guid TicketId { get; set; }
        public string ReservationCode { get; set; } = string.Empty;
    }
}
