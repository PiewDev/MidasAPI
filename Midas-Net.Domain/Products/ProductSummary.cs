using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Products
{
    public class ProductSummary
    {
        public string Type { get; set; }
        public string Product { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
