namespace CafeManagement.Models.OrderModel
{
    public class OrderStatusHistory
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Description { get; set; }
        public Enums.OrderStatus OldStatus {  get; set; }
        public Enums.OrderStatus NewStatus { get; set; }
        public Guid ChangeByUserId { get; set; }
        public Order Order { get; set; }
    }
}
