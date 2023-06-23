using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Crud
{
    public class CrudSupportAttribute:Attribute
    {
        public CrudSupport CrudSupport { get;}

        public CrudSupportAttribute(CrudSupport crudSupport)
        {
            CrudSupport = crudSupport;
        }
    }
}
