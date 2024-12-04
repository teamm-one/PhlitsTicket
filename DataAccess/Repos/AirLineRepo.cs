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
    public class AirLineRepo:BaseRepo<Airline>,AirLineIRepo
    {
        ApplicationDbContext _context;
        public AirLineRepo(ApplicationDbContext context) : base(context) {
        this._context=context;
        }
    }
}
