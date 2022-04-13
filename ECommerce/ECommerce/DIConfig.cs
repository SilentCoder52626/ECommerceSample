using ECommerce.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce
{
    public static class DIConfig
    {
        
        public static void UseEcommerceDI(this IServiceCollection services)
        {
            services.AddScoped<BrandServiceInterface, BrandService>();
            services.AddScoped<CategoryServiceInterface, CategoryService>();
            services.AddScoped<TagServiceInterface, TagService>();
            services.AddScoped<ProductServiceInterface, ProductService>();

        }
    }
}
