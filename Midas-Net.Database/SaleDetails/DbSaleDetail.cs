using Midas.Net.Database.Products;
using Midas.Net.Database.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.SaleDetails
{
    public class DbSaleDetail
    {
        public long SaleDetailId { get; set; }
        public long SaleId { get; set; }
        public DbSale Sale { get; set; }
        public long ProductId { get; set; }
        public DbProduct Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
