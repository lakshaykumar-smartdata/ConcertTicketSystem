using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using ConcertTicketSystem.Repositories.VenueRepo;

namespace ConcertTicketSystem.Services.VenueServices
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }
        public async Task<Guid> AddAsync(Venue venue)
        {
            return await _venueRepository.AddAsync(venue);
        }
        public async Task<IEnumerable<Venue>> GetAllAsync() =>
            await _venueRepository.GetAllAsync();

        public async Task<Venue> GetByIdAsync(Guid id) =>
            await _venueRepository.GetByIdAsync(id);

    }
}
