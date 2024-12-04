using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class AirLineFlights
    {
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
