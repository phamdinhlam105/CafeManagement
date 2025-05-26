namespace CafeManagement.Models.Stock
{
    public class AdjustmentDetail
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public Guid StockAdjustmentId {  get; set; }
        public Ingredient Ingredient { get; set; }
        public float QuantityAdjusted { get; set; }
        public decimal AdjustValue {  get; set; }
        public StockAdjustment StockAdjustment { get; set; }
    }
}
