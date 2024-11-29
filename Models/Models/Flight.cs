namespace Models.Models
{
    public class Flight
    {
        public int FlightID{ get; set; }
        public int AirlineID{ get; set; }
        public int DepartureAirportID{ get; set; }
        public int ArrivalAirportID{ get; set; }
        public DateTime DepartureTime{ get; set; }
        public DateTime ArrivalTime{ get; set; }
        public int Duration{ get; set; }
        public decimal Price{ get; set; }

        public Airline Airline{ get; set; }
        public Airport DepartureAirport{ get; set; }
        public Airport ArrivalAirport{ get; set; }
        public ICollection <Seat> Seats{ get; set; }
        public ICollection <Booking> Bookings{ get; set; }
    }

}
