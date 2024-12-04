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
    internal class FlightRepo:BaseRepo<Flight>,FlightIRepo
    {
        ApplicationDbContext _context;

        public FlightRepo(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
