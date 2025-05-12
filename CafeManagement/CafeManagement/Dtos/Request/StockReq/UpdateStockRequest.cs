namespace CafeManagement.Dtos.Request.Stock
{
    public class UpdateStockRequest
    {
        public Guid Id { get; set; }
        public IEnumerable<UpdateStockDetailRequest> Details { get; set; }

        public UpdateStockRequest()
        {
            Details = new List<UpdateStockDetailRequest>();
        }
    }
}
