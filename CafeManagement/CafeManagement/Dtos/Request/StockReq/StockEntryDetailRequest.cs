namespace CafeManagement.Dtos.Request.Stock
{
    public class StockEntryDetailRequest
    {
        public Guid IngredientId { get; set; }
        public float Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
