using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Respone.StockRes
{
    public class StockEntryDetailResponse
    {
        public Guid Id { get; set; }
        public Guid StockEntryId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName {  get; set; }
        public float ImportQuantity { get; set; }
        public float RemainQuantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => (decimal)ImportQuantity * Price;
    }
}
