using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midas.Net.Database.Products;
using Midas.Net.Domain.Products;
using Midas.Net.Domain.Sales;
using Midas.Net.Sales;
using Midas.Net.Service.Products;

namespace Midas.Net.Product
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {

        private readonly IProductsService _productsService;
        private readonly IMapper _mapper;
        public ProductsController(IProductsService productsService, IMapper mapper)
        {
            _productsService = productsService;
            _mapper = mapper;
        }

        [HttpGet("total-sales-by-product-type")]
        public async Task<ActionResult<List<ProductSummary>>> GetTotalSalesByProductType(DateTime date)
        {
            var productSales = await _productsService.GetProductSummariesByDate(date);

            return Ok(productSales);
        }

        [HttpGet("stock")]
        public async Task<ActionResult<Dictionary<string, int>>> GetProductStock()
        {
            var productStock = await _productsService.GetProductStock();
            return Ok(productStock);
        }
        public async Task<ActionResult<decimal?>> GetAveragePriceByProductType(long productTypeId)
        {
            var avgPrice = await _productsService.GetAveragePriceByProductType(productTypeId);
            if (avgPrice.HasValue)
                return Ok(avgPrice);
            else
                return NotFound();
        }
    }
}
