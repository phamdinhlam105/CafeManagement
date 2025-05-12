using Newtonsoft.Json;

namespace CafeManagement.Models.Stock
{
    public class DailyStockDetail
    {
        public Guid Id { get; set; }
        public float StockAtStartOfDay { get; set; }
        public float StockImport { get; set; }
        public float StockRemaining { get; set; }
        public Guid DailyStockId { get; set; }
        public Guid IngredientId {  get; set; }
        [JsonIgnore]
        public DailyStock? DailyStock { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
