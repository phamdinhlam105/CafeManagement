namespace CafeManagement.Models.Stock
{
    public class StockAdjustment
    {
        public Guid Id { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string Reason { get; set; } 
        public string AdjustedBy { get; set; } 
        public string Notes { get; set; }
        public IEnumerable<AdjustmentDetail> AdjustmentDetails { get; set; }
        public StockAdjustment()
        {
            AdjustmentDate = DateTime.UtcNow;
            AdjustmentDetails = new List<AdjustmentDetail>();
        }
    }
}
