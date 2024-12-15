using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Models.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime LeaveAt { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}
