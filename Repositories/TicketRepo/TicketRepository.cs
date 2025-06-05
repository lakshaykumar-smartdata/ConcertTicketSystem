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

        public async Task<List<Ticket>> GetByEventIdAsync(int eventId)
        {
            return await _context.Tickets
                .Where(t => t.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _context.Tickets
                .Include(t => t.Event)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task ReserveAsync(Ticket ticket)
        {
            ticket.Status = TicketStatus.Reserved.ToString();
            ticket.ReservedAt = DateTime.UtcNow;

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task PurchaseAsync(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket != null && ticket.Status == TicketStatus.Reserved.ToString())
            {
                ticket.Status = TicketStatus.Purchased.ToString();
                ticket.PurchasedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task CancelAsync(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket != null && ticket.Status == TicketStatus.Reserved.ToString())
            {
                ticket.Status = TicketStatus.Cancelled.ToString();
                await _context.SaveChangesAsync();
            }
        }
    }
}
