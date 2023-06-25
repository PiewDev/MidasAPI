using Midas.Net.Domain.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Products
{
    [CrudSupport]
    public class Product
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long TypeId { get; set; }
        public ProductType Type { get; set; }
        public int Stock { get; set; }
    }
}
