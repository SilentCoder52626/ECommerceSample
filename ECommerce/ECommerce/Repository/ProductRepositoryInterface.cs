using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public interface ProductRepositoryInterface
    {
        IQueryable<Product> GetQueryable();
        Task<Product?> GetById(long id);
        Task<IList<Product>> GetByCategoryId(long id);
        Task<IList<Product>> GetByBrandId(long id);
        Task<IList<Product>> GetByTagId(long id);
        Task<Product?> GetBySKU(string sku);
        Task<IList<Product>> GetAll();
        Task Insert(Product user);
        Task Update(Product user);
    }
}
