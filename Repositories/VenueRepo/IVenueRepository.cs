using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Repositories.VenueRepo
{
    public interface IVenueRepository
    {
        Task<Guid> AddAsync(Venue venue);
        Task<IEnumerable<Venue>> GetAllAsync();
        Task<Venue> GetByIdAsync(Guid id);
    }
}
