﻿using System.Net.Sockets;

namespace ConcertTicketSystem.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public decimal TicketPrice { get; set; }

        public List<Ticket> Tickets { get; set; } = new();
    }
}
