using ECommerce.Dto;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public interface ProductServiceInterface
    {
        Task<Product> Create(ProductCreateDto dto);
        Task Update(ProductUpdateDto dto);
        Task SetAsAvailable(long id);
        Task SetAsUnAvailable(long id);
        Task UpdatePrice(long id, decimal price);
    }
}
