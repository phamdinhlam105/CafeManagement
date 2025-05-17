using Newtonsoft.Json;

namespace CafeManagement.Models.Order
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentPrice {  get; set; }
        public string? Note { get; set; }
        public Product Product { get; set; }
        [JsonIgnore]
        public Order Order { get; set; }
    }
}
