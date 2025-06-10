using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Request.StockReq
{
    public class UpdateStockDetailRequest
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public float remainAmount { get; set; }
    }
}
