using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Models.ViewModels
{
    public class TripDataVM
    {
        public int? TripId { get; set; }
        public DateTime LeaveAt { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public int AirlineId { get; set; }
    }
}
