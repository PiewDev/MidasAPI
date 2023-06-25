using Midas.Net.Domain;
using Midas.Net.Domain.Crud;
using Midas.Net.Domain.Products;
using Midas.Net.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Service.Sales
{
    public class SaleService :ISaleService
    {
        private readonly ICrudRepository<Sale> _saleCrudRepository;

        private readonly IProductRepository _productRepository;

        private readonly ISaleRepository _saleRepository;

        public SaleService(ICrudRepository<Sale> saleCrudRepository, IProductRepository productRepository, ISaleRepository saleRepository )
        {
            _saleCrudRepository = saleCrudRepository;
            _productRepository = productRepository;
            _saleRepository = saleRepository;

        }
        public async Task<Sale> CreateSale(Sale sale)
        {
            var productIds = sale.SaleDetails.Select(sd => sd.ProductId).ToList();
            var products = await _productRepository.GetByIdAsync(productIds);

            var missingIds = new List<long>();
            var insufficientStockIds = new List<long>();

            foreach (var saleDetail in sale.SaleDetails)
            {
                var product = products.FirstOrDefault(p => p.ProductId == saleDetail.ProductId);
                if (product != null)
                {
                    saleDetail.UpdatePrice(product);

                    if (saleDetail.Quantity > product.Stock)
                    {
                        insufficientStockIds.Add(saleDetail.ProductId);
                    }
                }
                else
                {
                    missingIds.Add(saleDetail.ProductId);
                }
            }

            if (missingIds.Any() || insufficientStockIds.Any())
            {
                throw new SaleCreationException(missingIds, insufficientStockIds);
            }

            sale.SetDate(DateTime.Now);

            return await _saleCrudRepository.CreateAsync(sale);
        }
        public async Task<List<Sale>> GetSalesByDateAsync(DateTime date) 
        {
            return await _saleRepository.GetSalesByDateAsync(date);
        }
    }

}
