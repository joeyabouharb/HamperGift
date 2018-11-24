using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Infastructure.services
{
    public interface IDataService<T>
    {
        IQueryable<T> GetAll();
        Task Create(T entity);
        T GetSingle(Func<T, bool> predicate);
        IQueryable<T> Query(Func<T, bool> predicate);
        Task Update(T entity);
        Task Delete(T entity);

		Task AddMany(IEnumerable<T> entity);

		Task UpdateMany(IEnumerable<T> entity);

		Task RemoveMany(IEnumerable<T> entity);

	}
}
