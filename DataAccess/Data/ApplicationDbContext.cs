﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Trip> Trip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AirLineFlights>()
                .HasKey(e => new { e.AirlineId, e.FlightId });

            modelBuilder.Entity<Airline>()
                .HasOne(a => a.AirPortLeave)
                .WithMany(a => a.AirlineLeaves)
                .HasForeignKey(a => a.AirportLeaveId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Airline>()
                .HasOne(a => a.AirPortArrive)
                .WithMany(a => a.AirlineArrives)
                .HasForeignKey(a => a.AirPortArriveId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Airline>()
                .HasMany(a => a.AirlineFlights)
                .WithOne(af => af.Airline)
                .HasForeignKey(af => af.AirlineId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasMany(f => f.AirLineFlights)
                .WithOne(af => af.Flight)
                .HasForeignKey(af => af.FlightId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ApplicationUser)
                .WithMany()
                .HasForeignKey(b => b.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Seat)
                .WithMany()
                .HasForeignKey(b => b.SeatId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
