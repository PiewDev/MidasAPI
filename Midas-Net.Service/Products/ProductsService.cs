using Midas.Net.Domain;
using Midas.Net.Domain.Products;
using Midas.Net.Domain.Products.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Service.Products
{
    public class ProductService
    {
        private readonly IRepository<Product, long> _productRepository;

        public ProductService(IRepository<Product, long> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Dictionary<string, int>> GetProductStock()
        {
            var products = await _productRepository.GetAllAsync();
            var productStock = products.ToDictionary(p => p.Name, p => p.Stock);

            return productStock;
        }

        //public async Task<decimal?> GetAveragePriceByProductType(long productTypeId)
        //{
        //    var productsByType = await _productRepository.GetByType(productTypeId);
        //    var averagePrice = productsByType.Average(p => p.Price);

        //    return averagePrice;
        //}
    }

}
