using ConcertTicketSystem.Data;
using ConcertTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using static ConcertTicketSystem.Enums.Enum;

namespace ConcertTicketSystem.Repositories.TicketRepo
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ConcertTicketSystemDbContext _context;

        public TicketRepository(ConcertTicketSystemDbContext context)
        {
            _context = context;
        }
    }
}
