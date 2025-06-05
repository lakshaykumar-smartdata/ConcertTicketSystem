using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Services.TicketServices
{
    public interface ITicketService
    {
        Task<Guid> AddTicketTypeAsync(TicketType request);
        Task<bool> CancelReservationAsync(string reservationCode);
        Task<bool> PurchaseAsync(Guid ticketId, string reservationCode);
        Task<Ticket?> ReserveTicketAsync(Guid ticketTypeId, TimeSpan holdDuration);
    }
}
