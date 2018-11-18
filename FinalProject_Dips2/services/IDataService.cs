using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectUI.services
{
    public interface IDataService<T>
    {
        IQueryable<T> GetAll();
        void Create(T entity);
        T GetSingle(Func<T, bool> predicate);
        IQueryable<T> Query(Func<T, bool> predicate);
        void Update(T entity);
        void Delete(T entity);
   
    }
}
