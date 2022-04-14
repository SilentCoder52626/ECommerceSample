using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class DuplicateTagNameException : Exception
    {
        public DuplicateTagNameException(string name) : base($"Tag Name ({name}) already used.")
        {

        }
    }
}
