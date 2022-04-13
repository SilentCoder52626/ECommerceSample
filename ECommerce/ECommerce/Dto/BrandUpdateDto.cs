using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dto
{
    public class BrandUpdateDto : BrandCreateDto
    {
        public long BrandId { get; set; }
    }
}
