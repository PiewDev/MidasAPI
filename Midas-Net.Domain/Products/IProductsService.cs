using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Products
{
    public interface IProductsService
    {
        Task<Dictionary<string, int>> GetProductStock();
        Task<decimal?> GetAveragePriceByProductType(long productTypeId);
        Task<List<ProductSummary>> GetProductSummariesByDate(DateTime date);
    }
}
