using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException(string message = "Tag Not Found.") : base(message)
        {

        }
    }
}
