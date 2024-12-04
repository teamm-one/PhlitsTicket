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

            // تعيين المفتاح المركب بين AirlineId و FlightId في AirLineFlights
            modelBuilder.Entity<AirLineFlights>()
                .HasKey(e => new { e.AirlineId, e.FlightId });

            // العلاقة بين Airline و AirPortLeave
            modelBuilder.Entity<Airline>()
                .HasOne(a => a.AirPortLeave)
                .WithMany()
                .HasForeignKey(a => a.AirportLeaveId)
                .OnDelete(DeleteBehavior.NoAction); // لا نقوم بحذف البيانات المرتبطة عند حذف الطيران

            // العلاقة بين Airline و AirPortArrive
            modelBuilder.Entity<Airline>()
                .HasOne(a => a.AirPortArrive)
                .WithMany()
                .HasForeignKey(a => a.AirPortArriveId)
                .OnDelete(DeleteBehavior.NoAction); // لا نقوم بحذف البيانات المرتبطة عند حذف الطيران

            // العلاقة بين Airline و AirLineFlights
            modelBuilder.Entity<Airline>()
                .HasMany(a => a.AirlineFlights)
                .WithOne(af => af.Airline)
                .HasForeignKey(af => af.AirlineId)
                .OnDelete(DeleteBehavior.NoAction); // لا نقوم بحذف البيانات المرتبطة عند حذف الطيران

            // العلاقة بين Flight و AirLineFlights
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.AirLineFlights)
                .WithOne(af => af.Flight)
                .HasForeignKey(af => af.FlightId)
                .OnDelete(DeleteBehavior.NoAction); // لا نقوم بحذف البيانات المرتبطة عند حذف الطيران

            // العلاقة بين Booking و ApplicationUser
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.ApplicationUser)
                .WithMany()
                .HasForeignKey(b => b.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction); // لا نقوم بحذف البيانات المرتبطة عند حذف الحجز

            // العلاقة بين Booking و Payment - هنا يجب تحديد NoAction بدلاً من Cascade لتجنب الخطأ

            // العلاقة بين Booking و Seat
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Seat)
                .WithMany()
                .HasForeignKey(b => b.SeatId)
                .OnDelete(DeleteBehavior.NoAction); // لا نقوم بحذف البيانات المرتبطة عند حذف الحجز
        }
    }
}
