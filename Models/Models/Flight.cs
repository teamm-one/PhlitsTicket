namespace Models.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        
        public ICollection<Seat> Seats { get; set; }
        public ICollection<AirLineFlights> AirLineFlights { get; set; }
    }

}
