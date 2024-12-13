using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Airline
    {
        public int AirlineId { get; set; }
        public string Name { get; set; }
        [ForeignKey("AirPortLeave")]
        public int AirportLeaveId { get; set; }
        public Airport AirPortLeave { get; set; }
        [ForeignKey("AirPortArrive")]
        public int AirPortArriveId { get; set; }
        public Airport AirPortArrive { get; set; }
        public ICollection<AirLineFlights> AirlineFlights { get; set; }
    }

}
