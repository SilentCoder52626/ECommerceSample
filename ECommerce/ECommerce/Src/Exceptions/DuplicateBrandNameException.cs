using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Exceptions
{
    public class DuplicateBrandNameException : Exception
    {
        public DuplicateBrandNameException(string message) : base(GenerateMessage(message))
        {
        }

        private static string GenerateMessage(string message)
        {
            return $"Brand Name ({message}) Already Used.";
        }
    }
}
