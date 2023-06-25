using Midas.Net.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Sales
{
    public class SaleDetail
    {
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public void UpdatePrice(Product product)
        {
            UnitPrice = product.Price;
            TotalPrice = UnitPrice * Quantity;;
        }
    }
}
