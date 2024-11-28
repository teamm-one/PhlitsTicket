namespace Models.Models
{
    public class Airline
    {
        public int AirlineID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string IATA_Code { get; set; }

        public ICollection<Flight> Flights { get; set; }
    }

}
