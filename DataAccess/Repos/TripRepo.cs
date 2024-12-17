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
    public class TripRepo:BaseRepo<Trip> , TripIRepo
    {
        ApplicationDbContext _context;

        public TripRepo(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}
