using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.TicketRepo
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(List<Ticket> tickets);
        Task<Guid> AddTicketTypeAsync(TicketType request);
    }
}
