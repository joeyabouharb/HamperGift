
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Microsoft.EntityFrameworkCore;
namespace ProjectUI.services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        //fields
        private HamperDbContext _context;
        private DbSet<T> _dbSet;

        //constructor
        public DataService()
        {
            _context = new HamperDbContext();
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges(); // commit in TSQL
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;    
        }

        public T GetSingle(Func<T, bool> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> Query(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).AsQueryable();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

       
    }
}
