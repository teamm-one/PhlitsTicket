using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Models.ViewModels
{
    public class TripEditVM
    {
        public int TripId { get; set; }
        public DateTime LeaveAt { get; set; }
        public long Price { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int AirlineId { get; set; }
        public int FlightId { get; set; }
    }
}
