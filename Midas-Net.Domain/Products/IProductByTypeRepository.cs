using Midas.Net.Domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Products
{
    public interface IProductByTypeRepository
    {
        Task<List<Product>> GetProductsByTypeAsync(long typeId);
    }
}
