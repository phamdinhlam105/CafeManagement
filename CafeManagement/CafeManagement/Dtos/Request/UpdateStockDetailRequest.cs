using CafeManagement.Models.Stock;

namespace CafeManagement.Dtos.Request
{
    public class UpdateStockDetailRequest
    {
        public Guid Id { get; set; }
        public Ingredient Ingredient { get; set; }
        public float remainAmount {  get; set; }
    }
}
