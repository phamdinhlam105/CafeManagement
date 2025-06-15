using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Request.StockReq
{
    public class StockAdjustmentDetailRequest
    {
        public Guid IngredientId { get; set; }
        public float QuantityAdjusted { get; set; }
    }
}
