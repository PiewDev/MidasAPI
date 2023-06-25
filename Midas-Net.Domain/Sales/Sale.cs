namespace Midas.Net.Domain.Sales
{
    public class Sale
    {
        public long SaleId { get; set; }
        public DateTime Date { get; set; }
        public List<SaleDetail> SaleDetails { get; set; }
        public bool IsCancelled { get; set; }
        public void SetDate(DateTime date)
        {
            Date = date;
        }

    }
}
