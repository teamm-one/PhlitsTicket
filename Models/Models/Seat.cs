namespace Models.Models
{
    public class Seat
    {
        public int SeatID { get; set; }
        public string Class { get; set; }
        public bool Availability { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }

}
