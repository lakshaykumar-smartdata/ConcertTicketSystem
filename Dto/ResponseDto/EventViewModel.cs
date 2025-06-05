namespace ConcertTicketSystem.Dto.ResponseDto
{
    public class EventViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string VenueName { get; set; } = string.Empty;

        public List<TicketTypeViewModel> TicketTypes { get; set; } = new();
    }
}
