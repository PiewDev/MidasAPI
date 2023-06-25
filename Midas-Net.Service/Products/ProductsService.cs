using Midas.Net.Domain;
using Midas.Net.Domain.Crud;
using Midas.Net.Domain.Products;
using Midas.Net.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Service.Products
{
    public class ProductService
    {
        private readonly ICrudRepository<Product> _productCrudRepository;

        private readonly ISaleRepository _saleRepository;

        private readonly IProductRepository _productRepository;

        public ProductService(ICrudRepository<Product> productRepository)
        {
            _productCrudRepository = productRepository;
        }

        public async Task<Dictionary<string, int>> GetProductStock()
        {
            var products = await _productCrudRepository.GetAllAsync();
            var productStock = products.ToDictionary(p => p.Name, p => p.Stock);

            return productStock;
        }

        public async Task<decimal?> GetAveragePriceByProductType(long productTypeId)
        {
            var avgPrice = await _productRepository.GetAveragePriceByProductType(productTypeId);

            return avgPrice;
        }
        public async Task<List<ProductSummary>> GetProductSummariesByDate(DateTime date)
        {
            var sales = await _saleRepository.GetSalesByDateAsync(date);

            var productSummaries = new List<ProductSummary>();

            var productIds = sales.SelectMany(s => s.SaleDetails)
                                  .Select(sd => sd.ProductId)
                                  .Distinct()
                                  .ToList();

            var products = await _productRepository.GetByIdWithTypesAsync(productIds);

            foreach (var product in products)
            {
                var salesWithProduct = sales.Where(s => s.SaleDetails.Any(sd => sd.ProductId == product.ProductId));
                
                decimal totalAmount = salesWithProduct.Sum(s => s.SaleDetails.Where(sd => sd.ProductId == product.ProductId).Sum(sd => sd.TotalPrice));

                
                var summary = new ProductSummary
                {
                    Type = product.Type.Name,
                    Product = product.Name,
                    TotalAmount = totalAmount
                };

                productSummaries.Add(summary);
            }

            return productSummaries;

        }


    }
}
