using CafeManagement.Enums;

namespace CafeManagement.Dtos.Request.OrderReq
{
    public class NewOrderRequest
    {
        public string? Note { get; set; }
        public Guid? CustomerId { get; set; }
        public int No { get; set; }

    }
}
