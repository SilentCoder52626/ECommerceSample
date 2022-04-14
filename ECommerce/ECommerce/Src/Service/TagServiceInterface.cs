using ECommerce.Dto;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service
{
    public interface TagServiceInterface
    {
        Task<Tag> Create(TagCreateDto dto);
        Task Update(TagUpdateDto dto);
    }
}
