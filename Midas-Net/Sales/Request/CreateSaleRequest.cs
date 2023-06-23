using System.ComponentModel.DataAnnotations;

namespace Midas.Net.Sales
{
    public class CreateSaleRequest
    {
        [Required]
        public string Concepto { get; set; }

        [Required]
        public List<CreateSaleDetail> SaleDetails { get; set; }

    }
}
