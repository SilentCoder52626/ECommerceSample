using ECommerce.Dto;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public interface CategoryServiceInterface
    {
        Task<Category> Create(CategoryCreateDto dto);
        Task Update(CategoryUpdateDto dto);
    }
}
