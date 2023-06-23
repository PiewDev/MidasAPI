using System.ComponentModel.DataAnnotations;

namespace Midas.Net.Sales
{
    public class CreateSaleDetail
    {
        [Required]
        public long ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
