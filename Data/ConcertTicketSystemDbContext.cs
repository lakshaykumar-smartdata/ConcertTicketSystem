using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketSystem.Data
{
    /// <summary>
    /// Primary EF Core DbContext for the Concert Ticketing System.
    /// Manages entity mappings, relationships, and database access logic.
    /// </summary>
    public class ConcertTicketSystemDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the DbContext with the specified options.
        /// Used for dependency injection and configuration.
        /// </summary>
        public ConcertTicketSystemDbContext(DbContextOptions<ConcertTicketSystemDbContext> options)
            : base(options) { }

        // --- DbSet Definitions ---

        /// <summary>
        /// Represents the Events table (concerts/shows).
        /// </summary>
        public DbSet<Event> Events { get; set; }

        /// <summary>
        /// Represents the Tickets table (individual ticket instances).
        /// </summary>
        public DbSet<Ticket> Tickets { get; set; }

        /// <summary>
        /// Represents the Venues table (event locations).
        /// </summary>
        public DbSet<Venue> Venues { get; set; }

        /// <summary>
        /// Represents the TicketTypes table (e.g., VIP, Regular).
        /// </summary>
        public DbSet<TicketType> TicketTypes { get; set; }

        /// <summary>
        /// Configures entity relationships, constraints, and cascade behavior.
        /// Called automatically during model creation.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Relationship Mappings ---

            // Venue 1 --- * Event
            // A Venue can host multiple Events. Cascade delete propagates removal of a Venue to its Events.
            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Events)
                .WithOne(e => e.Venue)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Event 1 --- * TicketType
            // An Event can define multiple TicketTypes. Deleting an Event removes all its TicketTypes.
            modelBuilder.Entity<Event>()
                .HasMany(e => e.TicketTypes)
                .WithOne(tt => tt.Event)
                .HasForeignKey(tt => tt.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // TicketType 1 --- * Ticket
            // A TicketType can issue multiple Tickets. Deleting a TicketType removes all associated Tickets.
            modelBuilder.Entity<TicketType>()
                .HasMany(tt => tt.Tickets)
                .WithOne(t => t.TicketType)
                .HasForeignKey(t => t.TicketTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
