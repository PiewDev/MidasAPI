using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Midas.Net.Domain;
using Midas.Net.Domain.Sales;
using Midas.Net.Service.Sales;

namespace Midas.Net.Sales
{
    public class SalesByDateController : Controller
    {

        private readonly ISaleService _saleByDateService;
        private readonly IMapper _mapper;
        public SalesByDateController(ISaleService saleByDateService, IMapper mapper)
        {
            _saleByDateService = saleByDateService;
            _mapper = mapper;
        }
        [HttpGet("api/sales")]
        public async Task<IActionResult> GetSalesByDate(DateTime date)
        {
            var sales = await _saleByDateService.GetSalesByDateAsync(date);
            return Ok(sales);
        }

        [HttpPost("api/sales")]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            var sale = await _saleByDateService.CreateSale(_mapper.Map<Sale>(request));
            return Ok(sale);
        }
    }
}
