using CafeManagement.Enums;

namespace CafeManagement.Dtos.Request
{
    public class NewOrderRequest
    {
        public string? Note { get; set; }
        public Guid? CustomerId {  get; set; }
        public int No {  get; set; }

    }
}
