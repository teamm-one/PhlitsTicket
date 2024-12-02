﻿namespace Models.Models
{
    public class Airport
    {
        public int AirportID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public ICollection<Flight> DepartureFlights { get; set; }
        public ICollection<Flight> ArrivalFlights { get; set; }
    }

}
