using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain
{
    public class InternalException: Exception
    {
        public InternalException()
        {

        }
        public InternalException(string message) : base(message) { }
    }
}
