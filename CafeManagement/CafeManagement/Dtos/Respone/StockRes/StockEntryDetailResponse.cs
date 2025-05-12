using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Respone.Stock
{
    public class StockEntryDetailResponse
    {
        public Guid Id { get; set; }
        public Guid StockEntryId { get; set; }
        public Guid IngredientId { get; set; }
        public float Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => (decimal)Quantity * Price;
        public Ingredient Ingredient { get; set; }
    }
}
