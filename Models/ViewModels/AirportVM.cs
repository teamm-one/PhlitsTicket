using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class AirportVM
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public string City { get; set; }
    }
}
