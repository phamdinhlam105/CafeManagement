namespace CafeManagement.Dtos.Respone
{
    public class OrderDetailResponse
    {
        public Guid Id {  get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Total {  get; set; }
        public string? Note { get; set; }
    }
}
