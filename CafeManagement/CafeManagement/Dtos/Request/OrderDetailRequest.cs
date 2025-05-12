namespace CafeManagement.Dtos.Request
{
    public class OrderDetailRequest
    {
        public Guid OrderId { get; set; }
        public Guid ProductId {  get; set; }
        public string? Note {  get; set; }
        public int Quantity {  get; set; }
    }
}
