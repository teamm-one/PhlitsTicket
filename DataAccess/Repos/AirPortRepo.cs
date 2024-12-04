using DataAccess.Data;
using DataAccess.IRepos;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class AirPortRepo:BaseRepo<Airport>,AirPortIRepo
    {
        ApplicationDbContext _context;

        public AirPortRepo(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
