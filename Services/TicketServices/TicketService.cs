using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.TicketRepo;

namespace ConcertTicketSystem.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        public readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            // Step 1: Save the ticket type
            var ticketTypeId = await _ticketRepository.AddTicketTypeAsync(request);
            // Step 2: Generate physical tickets based on quantity
            var tickets = new List<Ticket>();
            for (int i = 0; i < request.QuantityAvailable; i++)
            {
                tickets.Add(new Ticket
                {
                    Id = Guid.NewGuid(),
                    TicketTypeId = ticketTypeId,
                    IsPurchased = false,
                    ReservationCode = string.Empty,
                    ReservationExpiresAt = null
                });
            }
            await _ticketRepository.AddTicketAsync(tickets);
            return ticketTypeId;
        }
    }
}
