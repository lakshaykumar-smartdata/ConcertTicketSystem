using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcertTicketSystem.Repositories.VenueRepo
{
    /// <summary>
    /// Repository implementation for managing Venue entities using EF Core DbContext.
    /// Provides asynchronous CRUD operations for the Venue aggregate.
    /// </summary>
    public class VenueRepository : IVenueRepository
    {
        private readonly ConcertTicketSystemDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="VenueRepository"/> class with the specified DbContext.
        /// </summary>
        /// <param name="concertTicketSystemDbContext">Database context injected via DI.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="concertTicketSystemDbContext"/> is null.</exception>
        public VenueRepository(ConcertTicketSystemDbContext concertTicketSystemDbContext)
        {
            _dbContext = concertTicketSystemDbContext ?? throw new ArgumentNullException(nameof(concertTicketSystemDbContext));
        }

        /// <summary>
        /// Adds a new Venue asynchronously and commits changes to the database.
        /// </summary>
        /// <param name="venue">The Venue entity to add.</param>
        /// <returns>Id of the newly created Venue.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="venue"/> is null.</exception>
        public async Task<Guid> AddAsync(Venue venue)
        {
            if (venue == null)
                throw new ArgumentNullException(nameof(venue));

            await _dbContext.Venues.AddAsync(venue);
            await _dbContext.SaveChangesAsync();
            return venue.Id;
        }

        /// <summary>
        /// Retrieves all Venue entities asynchronously.
        /// </summary>
        /// <returns>List of all venues.</returns>
        public async Task<IEnumerable<Venue>> GetAllAsync()
        {
            return await _dbContext.Venues
                .AsNoTracking() // Improves read performance since entities won't be tracked.
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a Venue by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">Unique identifier of the Venue.</param>
        /// <returns>Venue entity if found; otherwise, null.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="id"/> is empty Guid.</exception>
        public async Task<Venue> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Venue Id.", nameof(id));

            return await _dbContext.Venues.FindAsync(id);
        }
    }
}
