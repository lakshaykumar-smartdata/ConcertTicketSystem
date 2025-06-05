using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketSystem.Repositories.VenueRepo
{
    public class VenueRepository : IVenueRepository
    {
        private readonly ConcertTicketSystemDbContext _concertTicketSystemDbContext;
        public VenueRepository(ConcertTicketSystemDbContext concertTicketSystemDbContext)
        {
            _concertTicketSystemDbContext = concertTicketSystemDbContext;
        }
        public async Task<Guid> AddAsync(Venue venue)
        {
            await _concertTicketSystemDbContext.Venues.AddAsync(venue);
            await _concertTicketSystemDbContext.SaveChangesAsync();
            return venue.Id;
        }

        public async Task<IEnumerable<Venue>> GetAllAsync() =>
            await _concertTicketSystemDbContext.Venues.ToListAsync();

        public async Task<Venue> GetByIdAsync(Guid id) =>
            await _concertTicketSystemDbContext.Venues.FindAsync(id);
    }
}
