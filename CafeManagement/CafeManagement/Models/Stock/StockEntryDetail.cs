namespace CafeManagement.Models.Stock
{
    public class StockEntryDetail
    {
        public Guid Id { get; set; }
        public Guid StockEntryId { get; set; }
        public Guid IngredientId { get; set; }
        public float Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => (decimal)Quantity * Price;
        public StockEntry StockEntry { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
