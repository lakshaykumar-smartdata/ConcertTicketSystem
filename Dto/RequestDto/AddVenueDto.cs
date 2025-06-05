namespace ConcertTicketSystem.Dto.RequestDto
{
    public class AddVenueDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
}
