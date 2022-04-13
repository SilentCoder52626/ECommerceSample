using Common.Data.Repository;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerceSample.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Repository.ECommerce
{
    public class ProductRepository : BaseRepository<Product>, ProductRepositoryInterface
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IList<Product>> GetByBrandId(long id)
        {
            return await GetQueryable().Where(x => x.BrandId == id).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IList<Product>> GetByCategoryId(long id)
        {
            return await GetQueryable().Where(x => x.CategoryId == id).ToListAsync().ConfigureAwait(false);

        }

        public async Task<Product?> GetByName(string name)
        {
            return await GetQueryable().Where(a => a.Name.Equals(name)).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<Product?> GetBySKU(string uname)
        {
            return await GetQueryable().Where(a => a.SKU.Equals(uname)).SingleOrDefaultAsync().ConfigureAwait(false);

        }

        public async Task<IList<Product>> GetByTagId(long id)
        {
            return await GetQueryable().Where(x => x.TagId == id).ToListAsync().ConfigureAwait(false);

        }
    }
}
