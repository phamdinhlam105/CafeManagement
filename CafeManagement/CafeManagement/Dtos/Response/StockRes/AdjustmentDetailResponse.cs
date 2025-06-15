using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Response.StockRes
{
    public class AdjustmentDetailResponse
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public Guid StockAdjustmentId { get; set; }
        public string IngredientName { get; set; }
        public float QuantityAdjusted { get; set; }
        public decimal AdjustValue { get; set; }
    }
}
