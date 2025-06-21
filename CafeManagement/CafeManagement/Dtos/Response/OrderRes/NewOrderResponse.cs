using CafeManagement.Enums;
using CafeManagement.Models;

namespace CafeManagement.Dtos.Respone.OrderRes
{
    public class NewOrderResponse
    {
        public Guid Id { get; set; }
        public string? Note { get; set; }
        public int? TableNo { get; set; }
        public decimal Total { get; set; }
        public int Amount { get; set; }
        public OrderStatus Status { get; set; }
        public string CustomerName { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? PromotionId { get; set; }
        public Guid CreatedByUserId {  get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid LastUpdateByUserId { get; set; }
        public string LastUpdateByUserName { get; set; }
        public DateTime LastUpdateAt { get; set; }
        public List<OrderDetailResponse> Details { get; set; }

    }
}
