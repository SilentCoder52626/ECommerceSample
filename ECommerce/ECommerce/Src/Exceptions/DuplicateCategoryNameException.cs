using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class DuplicateCategoryNameException : Exception
    {
        public DuplicateCategoryNameException(string name) : base($"Category Name - {name} Already Used.")
        {

        }
    }
}
