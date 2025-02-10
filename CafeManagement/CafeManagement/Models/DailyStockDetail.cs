namespace CafeManagement.Models
{
    public class DailyStockDetail
    {
        public Guid Id { get; set; }
        public float StockAtStartOfDay { get; set; }
        public float StockImport { get; set; }
        public float StockRemaining { get; set; }
        public DailyStock DailyStock { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
