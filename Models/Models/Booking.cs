using Utility;
namespace Models.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public string ApplicationUserId { get; set; }
        public int SeatId { get; set; }
        public int TripId { get; set; }

        public Trip Trip { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Seat Seat { get; set; }
    }

}
