using Utility;
namespace Models.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public Status Status { get; set; }
        public DateTime BookingDate { get; set; }
        public string ApplicationUserId { get; set; }
        public int SeatId { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Seat Seat { get; set; }
    }

}
