namespace CafeManagement.Models
{
    public class StockEntryDetail
    {
        public Guid Id { get; set; } 
        public Guid StockEntryId { get; set; }
        public Guid IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => Quantity * Price;

        public StockEntry StockEntry { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
