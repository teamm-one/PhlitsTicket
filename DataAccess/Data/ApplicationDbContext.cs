using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<AirLineFlights> AirLineFlights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //relations
            modelBuilder.Entity<Airline>()
            .HasOne(a => a.AirPortLeave)
            .WithMany(a => a.AirlineLeaves)
            .HasForeignKey(a => a.AirportLeaveId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Airline>()
                .HasOne(a => a.AirPortArrive)
                .WithMany(a => a.AirlineArrives)
                .HasForeignKey(a => a.AirPortArriveId);

            modelBuilder.Entity<AirLineFlights>()
                .HasKey(e => new { e.AirlineId, e.FlightId });

            modelBuilder.Entity<Airline>()
                .HasMany(a => a.AirlineFlights)
                .WithOne(a => a.Airline)
                .HasForeignKey(a => a.AirlineId);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.AirLineFlights)
                .WithOne(af => af.Flight)
                .HasForeignKey(af => af.FlightId);
        }
    }
}
