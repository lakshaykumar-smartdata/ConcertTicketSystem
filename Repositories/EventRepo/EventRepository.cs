using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConcertTicketSystem.Repositories.EventRepo
{
    public class EventRepository : IEventRepository
    {
        private readonly ConcertTicketSystemDbContext _context;

        public EventRepository(ConcertTicketSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _context.Events
                                 .Include(e => e.Tickets)
                                 .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events
                                 .Include(e => e.Tickets)
                                 .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Event evnt)
        {
            await _context.Events.AddAsync(evnt);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Event evnt)
        {
            _context.Events.Update(evnt);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var evnt = await _context.Events.FindAsync(id);
            if (evnt != null)
            {
                _context.Events.Remove(evnt);
                await _context.SaveChangesAsync();
            }
        }
    }
}
