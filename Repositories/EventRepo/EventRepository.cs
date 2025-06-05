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
        public async Task<Guid> AddAsync(Event request)
        {
            await _context.Events.AddAsync(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetByIdAsync(Guid id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Guid> AddTicketTypeAsync(TicketType request)
        {
            await _context.TicketTypes.AddAsync(request);
            await _context.SaveChangesAsync();
            return request.Id;
        }
    }
}
