using Midas.Net.Database.Products;
using Midas.Net.Database.SaleDetails;
using Midas.Net.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Database.Sales
{
    [DbMapping(typeof(Sale))]
    public class DbSale
    {
        public long SaleId { get; set; }
        public DateTime Date { get; set; }
        public List<DbSaleDetail> SaleDetails { get; set; }
        public bool IsCancelled { get; set; }

    }
}
