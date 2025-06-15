namespace CafeManagement.Dtos.Response.StockRes
{
    public class StockAdjustmentResponse
    {
        public Guid Id {  get; set; }
        public DateTime CreatedAt {  get; set; }
        public string AdjustedBy { get; set; }
        public string Reason {  get; set; }
        public string Note {  get; set; }
        public List<AdjustmentDetailResponse> Details { get; set; }

    }
}
