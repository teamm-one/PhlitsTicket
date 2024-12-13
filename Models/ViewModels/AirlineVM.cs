using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class AirlineVM
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AirportLeaveId { get; set; }
        [Required]
        public int AirPortArriveId { get; set; }
    }
}
