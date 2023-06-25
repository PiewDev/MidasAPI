using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Products
{
    public interface IProductRepository
    {
        Task<decimal> GetAveragePriceByProductType(long productTypeId);
        Task<List<Product>> GetByIdAsync(List<long> idEntity);
        Task<List<Product>> GetByIdWithTypesAsync(List<long> idsEntity);
    }
}
