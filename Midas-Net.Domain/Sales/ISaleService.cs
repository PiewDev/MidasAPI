using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Sales
{
    public interface ISaleService
    {
        Task<List<Sale>> GetSalesByDateAsync(DateTime date);
        Task<Sale> CreateSale(Sale sale);
    }
}
