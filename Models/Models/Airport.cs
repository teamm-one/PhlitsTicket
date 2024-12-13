namespace Models.Models
{
    public class Airport
    {
        public int AirportId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        
        public ICollection<Airline>  AirlineArrives { get; set;}
        public ICollection<Airline> AirlineLeaves { get; set; }
    }

}
