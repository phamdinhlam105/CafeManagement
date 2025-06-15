using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Respone.StockRes
{
    public class StockDetailResponse
    {
        public Guid Id { get; set; }
        public float StockUsage { get; set; }
        public float StockImport { get; set; }
        public float StockRemaining { get; set; }
        public Guid DailyStockId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
    }
}
