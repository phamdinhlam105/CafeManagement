namespace CafeManagement.Models.Stock
{
    public class StockUsageDetail
    {
        public Guid Id { get; set; }
        public Guid StockEntryDetailId { get; set; }  
        public Guid StockUsageLogId {  get; set; }
        public double QuantityUsed { get; set; }
        public decimal TotalValue { get; set; } 
        public Guid UsageLogId { get; set; }
        public StockEntryDetail StockEntryDetail { get; set; }
        public StockUsageLog StockUsageLog { get; set; }
    }
}
