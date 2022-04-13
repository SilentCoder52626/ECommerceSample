using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity
{
    public class Tag
    {
        protected Tag()
        {

        }
        public Tag(string name)
        {
            this.Name = name;
        }
        public void Update(string name)
        {
            Name = name;
        }
        public long TagId { get; protected set; }
        public string Name { get; protected set; }
        public ICollection<Product> Products { get; protected set; } = new List<Product>();

    }
}
