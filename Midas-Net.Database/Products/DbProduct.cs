using Midas.Net.Database.ProductTypes;
using Midas.Net.Domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Products
{
    [DbMapping(typeof(Product))]
    public class DbProduct
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long TypeId { get; set; }
        public DbProductType Type { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }

    }

}
