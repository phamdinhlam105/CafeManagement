using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Request.Stock
{
    public class UpdateStockDetailRequest
    {
        public Guid Id { get; set; }
        public Guid IngredientId { get; set; }
        public float remainAmount { get; set; }
    }
}
