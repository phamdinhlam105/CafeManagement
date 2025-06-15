namespace CafeManagement.Dtos.Request.StockReq
{
    public class StockAdjustmentRequest
    {
        public string Reason { get; set; }
        public string AdjustedBy { get; set; }
        public string Notes { get; set; }
        public IEnumerable<StockAdjustmentDetailRequest> Details { get; set; }

        public StockAdjustmentRequest()
        {
            Details = new List<StockAdjustmentDetailRequest>();
        }
    }
}
