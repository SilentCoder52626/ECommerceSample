using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity
{
    public class Brand
    {
        protected Brand()
        {

        }
        public Brand(string name)
        {
            Name = name;
        }
        public void Update(string name)
        {
            Name = name;
        }
        public long BrandId { get; protected set; }
        public string Name { get; protected set; }
        public ICollection<Product> Products { get; protected set; } = new List<Product>();
    }
}
