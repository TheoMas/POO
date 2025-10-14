using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    internal class ArmoryException: Exception
    {
        public ArmoryException() { }

        public ArmoryException(string message) : base(message)
        {
        }

        public ArmoryException(string message, Exception inner ) : base(message, inner) 
        {
        }
    }
}
