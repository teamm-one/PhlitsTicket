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
       // public int  MyProperty { get; set; }
        public string? ImgUrl { get; set; }
        public string? PhoneNumber {  get; set; }
        public StaticData UserType {  get; set; }
        public string? PassborNumber { get; set; }
    }
}
