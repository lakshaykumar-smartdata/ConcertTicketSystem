using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.VenueRepo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConcertTicketSystem.Services.VenueServices
{
    /// <summary>
    /// Provides business logic for managing venues.
    /// </summary>
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="VenueService"/>.
        /// </summary>
        /// <param name="venueRepository">Injected venue repository dependency.</param>
        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository ?? throw new ArgumentNullException(nameof(venueRepository));
        }

        /// <inheritdoc />
        public async Task<Guid> AddAsync(Venue venue)
        {
            if (venue == null)
                throw new ArgumentNullException(nameof(venue));

            return await _venueRepository.AddAsync(venue);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Venue>> GetAllAsync()
        {
            return await _venueRepository.GetAllAsync();
        }

        /// <inheritdoc />
        public async Task<Venue> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Venue ID cannot be empty.", nameof(id));

            return await _venueRepository.GetByIdAsync(id);
        }
    }
}
