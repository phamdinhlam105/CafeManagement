using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Respone.StockRes
{
    public class StockEntryResponse
    {
        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal TotalValue { get; set; }
        public ICollection<StockEntryDetailResponse> Details { get; set; }
        public StockEntryResponse()
        {
            Details = new List<StockEntryDetailResponse>();
        }
    }
}
