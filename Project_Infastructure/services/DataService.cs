
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
//..
using Microsoft.EntityFrameworkCore;
namespace Project_Infastructure.services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        //fields
        private DesignDbContext _context;
        private DbSet<T> _dbSet;

        //constructor
        public DataService(DesignDbContext context)
        {
			_context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
           await _dbSet.AddAsync(entity);
           await _context.SaveChangesAsync(); // commit in TSQL
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
           await _context.SaveChangesAsync();
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

        public async Task Update(T entity)
        {
             _dbSet.Update(entity);
		
            await _context.SaveChangesAsync();

        }
		public async Task AddMany(IEnumerable<T> entity)
		{

			await _dbSet.AddRangeAsync(entity);

			await _context.SaveChangesAsync();

		}

		public async Task UpdateMany(IEnumerable<T> entity)
		{
			
			_dbSet.UpdateRange(entity);
			await _context.SaveChangesAsync();
		}

		public async Task RemoveMany(IEnumerable<T> entity)
		{
			_dbSet.RemoveRange(entity);
			await _context.SaveChangesAsync();
		}

    }
}
