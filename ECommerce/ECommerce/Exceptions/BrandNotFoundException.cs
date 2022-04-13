using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class BrandNotFoundException : Exception
    {
        public BrandNotFoundException(string message = "Brand Not Found.") : base(message)
        {

        }
    }
}
