namespace Models.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Name { get; set; }
        
        public ICollection<Seat> Seats { get; set; }
        public ICollection<AirLineFlights> AirLineFlights { get; set; }
    }

}
