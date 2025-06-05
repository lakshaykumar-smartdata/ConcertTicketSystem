using ConcertTicketSystem.Data;
using ConcertTicketSystem.Repositories.EventRepo;
using ConcertTicketSystem.Repositories.TicketRepo;
using ConcertTicketSystem.Repositories.VenueRepo;
using ConcertTicketSystem.Services.EventServices;
using ConcertTicketSystem.Services.TicketServices;
using ConcertTicketSystem.Services.VenueServices;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketSystem.DependencyInjection
{
    /// <summary>
    /// Extension methods for registering Concert Ticket System dependencies into the DI container.
    /// Encapsulates DbContext, repositories, and service registrations for modular startup configuration.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the core services, repositories, and EF DbContext needed by the Concert Ticket System.
        /// Centralizes all DI bindings for maintainability and separation of concerns.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <param name="configuration">Application configuration providing connection strings, etc.</param>
        /// <returns>Updated IServiceCollection for chaining.</returns>
        public static IServiceCollection AddConcertTicketSystemServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register EF Core DbContext with SQL Server provider.
            // Uses connection string from configuration for environment-specific setups.
            services.AddDbContext<ConcertTicketSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repository registrations with Scoped lifetime for per-request scope.
            // Facilitates clean separation of data access layer.
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            // Domain service registrations encapsulating business logic.
            // Scoped lifetime aligns with repository and DbContext lifetimes.
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IVenueService, VenueService>();
            services.AddScoped<ITicketService, TicketService>();

            return services;
        }
    }
}
