using ConcertTicketSystem.Models;

namespace ConcertTicketSystem.Services.VenueServices
{
    public interface IVenueService
    {
        Task<Guid> AddAsync(Venue venue);
        Task<IEnumerable<Venue>> GetAllAsync();
        Task<Venue> GetByIdAsync(Guid id);
    }
}
