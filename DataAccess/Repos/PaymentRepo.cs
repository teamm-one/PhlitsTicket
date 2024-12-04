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
    internal class PaymentRepo:BaseRepo<Payment>,PaymentIRepo
    {
        ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
    }
}
