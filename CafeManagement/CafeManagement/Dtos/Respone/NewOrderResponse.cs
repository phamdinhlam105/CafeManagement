using CafeManagement.Enums;
using CafeManagement.Models;

namespace CafeManagement.Dtos.Respone
{
    public class NewOrderResponse
    {
        public Guid Id {  get; set; }
        public string? Note {  get; set; }
        public int? No {  get; set; }
        public decimal Total {  get; set; }
        public int Amount {  get; set; }
        public OrderStatus Status { get; set; }
        public string CustomerName {  get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PromotionId {  get; set; }
        public DateTime CreatedAt {  get; set; }
        public ICollection<OrderDetailResponse> Details { get; set; }
        
    }
}
