using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dto
{
    public class CategoryUpdateDto : CategoryCreateDto
    {
        public long CategoryId { get; set; }
    }
}
