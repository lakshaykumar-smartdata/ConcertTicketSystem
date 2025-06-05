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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                        .HasMany(e => e.Tickets)
                        .WithOne(t => t.Event)
                        .HasForeignKey(t => t.EventId);
        }
    }
}
