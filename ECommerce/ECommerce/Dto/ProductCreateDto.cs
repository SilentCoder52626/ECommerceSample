using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Dto
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public long BrandId { get; set; }
        public long CategoryId { get; set; }
        public long TagId { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }

    }
}
