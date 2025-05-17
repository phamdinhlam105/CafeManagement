namespace CafeManagement.Dtos.Request.OrderReq
{
    public class OrderDetailRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string? Note { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
