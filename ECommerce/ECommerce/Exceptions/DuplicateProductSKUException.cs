using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class DuplicateProductSKUException : Exception
    {
        public DuplicateProductSKUException(string sku):base($"Product with same sku - {sku} already exists.")
        {

        }
    }
}
