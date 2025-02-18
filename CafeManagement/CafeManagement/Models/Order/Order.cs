using CafeManagement.Enums;

namespace CafeManagement.Models.Order
{
    public abstract class Order
    {
        public Guid Id { get; set; }
        public int No { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime createdAt { get; set; }
        public Guid CustomerId { get; set; }
        public string Note { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public OrderType OrderType { get; set; }
        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
        public ICollection<OrderStatusHistory> StatusHistories { get; set; }
        public Customer Customer { get; set; }
        public Order()
        {
            Details = new List<OrderDetail>();
            StatusHistories = new List<OrderStatusHistory>();
            Price = 0;
            Quantity = 0;
            createdAt = DateTime.Now;
        }
    }
}
