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
    public class CategoryRepository : BaseRepository<Category>, CategoryRepositoryInterface
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Category?> GetByName(string name)
        {
            return await GetQueryable().Where(a => a.Name.Equals(name)).SingleOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
