namespace Models.Models
{
    public class Airline
    {
        public int AirlineID { get; set; }
        public string Name { get; set; }

        public int AirportLeaveId { get; set; }
        public Airport AirPortLeave { get; set; }
        public int AirPortArriveId { get; set; }
        public Airport AirPortArrive { get; set; }
        public ICollection<AirLineFlights> AirlineFlights { get; set; }
    }

}
