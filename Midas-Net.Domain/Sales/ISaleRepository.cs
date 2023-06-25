using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Sales
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetSalesByDateAsync(DateTime date);
    }
}
