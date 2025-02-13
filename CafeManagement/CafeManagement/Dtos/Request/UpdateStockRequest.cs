namespace CafeManagement.Dtos.Request
{
    public class UpdateStockRequest
    {
        public Guid Id {  get; set; }
        public IEnumerable<UpdateStockDetailRequest> Details { get; set; }

        public UpdateStockRequest() 
        {
            Details = new List<UpdateStockDetailRequest>();
        }
    }
}
