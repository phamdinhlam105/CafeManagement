using CafeManagement.Enums;
using CafeManagement.Models.PromotionModel;

namespace CafeManagement.Models.Order
{
    public class Order
    {
        public Guid Id { get; set; }
        public int No { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime createdAt { get; set; }
        public Guid? CustomerId { get; set; }
        public string Note { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Guid? PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
        public Customer Customer { get; set; }
        public Order()
        {
            Details = new List<OrderDetail>();
            Price = 0;
            Quantity = 0;
            createdAt = DateTime.Now;
        }
    }
}
