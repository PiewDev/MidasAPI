using Midas.Net.Domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Sales
{
    public class Sale
    {
        public DateTime Date { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
        public void SetDate(DateTime date)
        {
            Date = date;
        }

    }
}
