using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.IRepos;
using DataAccess.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    internal class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DbContext _context;
        public BaseRepo(DbContext context)
        {
            _context = context;
        }

        IEnumerable<T> IBaseRepo<T>.GetAll()
        {
           return _context.Set<T>().ToList();
        }

        T IBaseRepo<T>.GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        void IBaseRepo<T>.Create(T entity)
        { 
               _context.Set<T>().Add(entity);
              _context.SaveChanges();
        }

    

        void IBaseRepo<T>.Edit(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        void IBaseRepo<T>.Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

       
    }
}
