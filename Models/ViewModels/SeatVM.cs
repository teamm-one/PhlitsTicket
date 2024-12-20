using Models.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class SeatVM
    {
        [AllowNull]
        public int SeatId { get; set; }
        public string Class { get; set; }
        public bool Availability { get; set; }
        public int FlightId { get; set; }
    }
}
