using Midas.Net.Domain.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Products.DTO
{
    [CrudSupport(CrudSupport.Supported)]
    public class ProductType
    {
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
    }
}
