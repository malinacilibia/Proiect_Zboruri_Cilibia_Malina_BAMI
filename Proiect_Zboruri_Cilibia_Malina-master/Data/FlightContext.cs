using Microsoft.EntityFrameworkCore;
using Proiect_Zboruri_Cilibia_Malina.Models;

namespace Proiect_Zboruri_Cilibia_Malina.Data
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options)
        {
        }

        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TicketClass> TicketClasses { get; set; }
        public DbSet<SatisfactionPredictionHistory> PredictionHistories { get; set; }
    }
}
