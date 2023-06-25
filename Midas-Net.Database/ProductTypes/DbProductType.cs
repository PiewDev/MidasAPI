using Midas.Net.Database.Products;
using Midas.Net.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.ProductTypes
{
    [DbMapping(typeof(ProductType))]
    public class DbProductType
    {
        public long ProductTypeId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<DbProduct> Products { get; set; }
    }
}
