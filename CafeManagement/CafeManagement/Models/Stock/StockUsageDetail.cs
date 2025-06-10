namespace CafeManagement.Models.Stock
{
    public class StockUsageDetail
    {
        public Guid Id { get; set; }
        public Guid StockEntryDetailId { get; set; }  
        public Guid StockUsageLogId {  get; set; }
        public float QuantityUsed { get; set; }
        public decimal TotalValue { get; set; } 
        public Guid UsageLogId { get; set; }
        public StockEntryDetail StockEntryDetail { get; set; }
        public StockUsageLog StockUsageLog { get; set; }
    }
}
