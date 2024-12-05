using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    
    public class ApplicationUser:IdentityUser
    {
       
        public string? ImgUrl { get; set; }
        public ICollection<Payment>? Payments { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
