using Midas.Net.Domain;
using Midas.Net.Domain.Products.DTO;
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
        private readonly IRepository<Sale, long> _saleRepository;

        private readonly IRepository<Product, long> _productRepository;

        public SaleService(IRepository<Sale, long> saleRepository, IRepository<Product, long> productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }
        public async Task<Sale> CreateSale(Sale sale)
        {
            var productsId = sale.SaleDetails.Select(sd => sd.ProductId).ToList();
            var products = await _productRepository.FindByAsync(entity => productsId.Contains(((Product)entity).ProductId));

            List<long> missingIds = new List<long>();

            foreach (var saleDetail in sale.SaleDetails)
            {
                var product = products.FirstOrDefault(p => p.ProductId == saleDetail.ProductId);
                if (product != null)
                {
                    saleDetail.UpdatePrice(product);
                }
                else
                {
                    missingIds.Add(saleDetail.ProductId);
                }
            }

            if (missingIds.Any())
            {
                throw new MissingSaleIdsException(missingIds);
            }

            sale.SetDate(DateTime.Now);

            return await _saleRepository.CreateAsync(sale);            

        }
        public async Task<List<Sale>> GetSalesByDateAsync(DateTime date) 
        {
            var sales = await _saleRepository.FindByAsync(s => ((Sale)s).Date == date);
            return sales.ToList();
        }
    }

}
