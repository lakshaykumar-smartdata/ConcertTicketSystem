using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.TicketRepo
{
    public interface ITicketRepository
    {
        Task AddTicketAsync(List<Ticket> tickets);
        Task<Ticket> GetAvailableTicket(Guid ticketTypeId);
        Task<Guid> AddTicketTypeAsync(TicketType request);
        Task UpdateTicketAsync(Ticket ticket);
        Task<Ticket> GetTicketByIdAsync(Guid ticketId);
    }
}
