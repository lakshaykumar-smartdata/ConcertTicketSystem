using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static ConcertTicketSystem.Enums.Enum;

namespace ConcertTicketSystem.Repositories.TicketRepo
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ConcertTicketSystemDbContext _context;

        public TicketRepository(ConcertTicketSystemDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            await _context.TicketTypes.AddAsync(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }
        public async Task AddTicketAsync(List<Ticket> tickets)
        {
            await _context.Tickets.AddRangeAsync(tickets);
            await _context.SaveChangesAsync();
        }
        public async Task<Ticket> GetAvailableTicket(Guid ticketTypeId)
        {
            return await _context.Tickets
            .Where(t => t.TicketTypeId == ticketTypeId &&
                        !t.IsPurchased &&
                        (t.ReservationExpiresAt == null || t.ReservationExpiresAt < DateTime.UtcNow))
            .FirstOrDefaultAsync();
        }
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
        public async Task<Ticket> GetTicketByIdAsync(Guid ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId);
        }
    }
}
