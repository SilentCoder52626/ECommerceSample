using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class DuplicateBrandNameException : Exception
    {
        public DuplicateBrandNameException(string name) : base($"Brand Name - {name} Already Used.")
        {

        }
    }
}
