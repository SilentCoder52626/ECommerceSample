using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModule.BaseRepo
{
   public  interface BaseRepositoryInterface<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task Insert(T entity);
        Task InsertRange(IList<T> entities);
        Task Update(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetQueryable();
        Task<T?> GetById(long id);
    }
}
