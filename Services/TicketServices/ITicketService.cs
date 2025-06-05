using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Services.TicketServices
{
    public interface ITicketService
    {
        Task<Guid> AddTicketTypeAsync(TicketType request);
    }
}
