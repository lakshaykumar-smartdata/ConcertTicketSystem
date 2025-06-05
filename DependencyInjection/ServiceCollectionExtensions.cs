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
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConcertTicketSystemServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext registration
            services.AddDbContext<ConcertTicketSystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            // Services
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IVenueService, VenueService>();
            services.AddScoped<ITicketService, TicketService>();

            return services;
        }
    }
}
