using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message = "Product Not Found.") : base(message)
        {

        }
    }
}
