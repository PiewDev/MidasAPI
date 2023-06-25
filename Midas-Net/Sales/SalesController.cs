using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Midas.Net.Domain;
using Midas.Net.Domain.Sales;
using Midas.Net.Service.Sales;

namespace Midas.Net.Sales
{
    [Authorize]

    [Route("api/[controller]")]
    public class SalesController : Controller
    {

        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;
        public SalesController(ISaleService saleService, IMapper mapper)
        {
            _saleService = saleService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetSalesByDate(DateTime date)
        {
            var sales = await _saleService.GetSalesByDateAsync(date);
            return Ok(sales);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request)
        {
            try
            {
                var sale = await _saleService.CreateSale(_mapper.Map<Sale>(request));
                return Ok(sale);
            }
            catch (SaleCreationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
