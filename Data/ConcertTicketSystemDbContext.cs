using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConcertTicketSystem.Data
{
    public class ConcertTicketSystemDbContext : DbContext
    {
        public ConcertTicketSystemDbContext(DbContextOptions<ConcertTicketSystemDbContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Venue - Event one-to-many
            modelBuilder.Entity<Venue>()
                .HasMany(v => v.Events)
                .WithOne(e => e.Venue)
                .HasForeignKey(e => e.VenueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Event - TicketType one-to-many
            modelBuilder.Entity<Event>()
                .HasMany(e => e.TicketTypes)
                .WithOne(tt => tt.Event)
                .HasForeignKey(tt => tt.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // TicketType - Ticket one-to-many
            modelBuilder.Entity<TicketType>()
                .HasMany(tt => tt.Tickets)
                .WithOne(t => t.TicketType)
                .HasForeignKey(t => t.TicketTypeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
