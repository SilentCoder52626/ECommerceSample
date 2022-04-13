using Common.Data.Repository;
using ECommerce.Data.Repository.ECommerce;
using ECommerce.Repository;
using ECommerce.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data
{
    public static class DIConfig
    {

        public static void UseDataDI(this IServiceCollection services)
        {
            services.AddScoped<BrandRepositoryInterface, BrandRepository>();
            services.AddScoped<CategoryRepositoryInterface, CategoryRepository>();
            services.AddScoped<TagRepositoryInterface, TagRepository>();
            services.AddScoped<ProductRepositoryInterface, ProductRepository>();
        }
    }
}
