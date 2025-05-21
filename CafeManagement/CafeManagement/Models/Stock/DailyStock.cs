using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.Stock
{
    public class DailyStock
    {
        public Guid Id { get; set; }
        public float StockAtStartOfDay { get; set; }
        public float StockImport { get; set; }
        public float StockRemaining { get; set; }
        public Guid IngredientId { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly CreateDate { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
