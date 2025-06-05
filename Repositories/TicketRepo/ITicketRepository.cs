using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.TicketRepo
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetByEventIdAsync(int eventId);
        Task<Ticket?> GetByIdAsync(int id);
        Task ReserveAsync(Ticket ticket);
        Task PurchaseAsync(int ticketId);
        Task CancelAsync(int ticketId);
    }
}
