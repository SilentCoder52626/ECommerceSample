using ECommerce.Dto;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public interface BrandServiceInterface
    {
        Task<Brand> Create(BrandCreateDto dto);
        Task Update(BrandUpdateDto dto);
        
    }
}
