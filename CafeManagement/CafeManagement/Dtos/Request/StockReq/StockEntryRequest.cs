namespace CafeManagement.Dtos.Request.StockReq
{
    public class StockEntryRequest
    {
        public decimal TotalValue { get; set; }
        public ICollection<StockEntryDetailRequest> Details { get; set; }
        public StockEntryRequest()
        {
            Details = new List<StockEntryDetailRequest>();
        }
    }
}
