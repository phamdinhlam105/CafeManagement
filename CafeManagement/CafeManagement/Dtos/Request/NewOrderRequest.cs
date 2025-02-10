using CafeManagement.Enums;

namespace CafeManagement.Dtos.Request
{
    public class NewOrderRequest
    {
        public OrderType OrderType { get; set; }
        public string? Note { get; set; }
        public Guid CustomerId {  get; set; }

    }
}
