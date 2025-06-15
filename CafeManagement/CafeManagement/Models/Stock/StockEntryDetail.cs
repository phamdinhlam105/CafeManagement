using Newtonsoft.Json;

namespace CafeManagement.Models.Stock
{
    public class StockEntryDetail
    {
        public Guid Id { get; set; }
        public Guid StockEntryId { get; set; }
        public Guid IngredientId { get; set; }
        public float ImportQuantity { get; set; }
        public float RemainQuantity {  get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => (decimal)ImportQuantity * Price;
        public StockEntry StockEntry { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
