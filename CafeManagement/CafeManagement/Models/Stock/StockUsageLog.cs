namespace CafeManagement.Models.Stock
{
    public class StockUsageLog
    {
        public Guid Id { get; set; }
        public DateTime UsedAt { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId {  get; set; }
        public decimal SellingPrice {  get; set; }
        public decimal TotalCost {  get; set; }
        public int Quantity { get; set; }
        public List<StockUsageDetail> StockUsageDetails { get; set; }
        public StockUsageLog()
        {
            UsedAt = DateTime.UtcNow;
            StockUsageDetails = new List<StockUsageDetail>();
        }
    }
}
