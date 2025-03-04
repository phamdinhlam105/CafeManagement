namespace CafeManagement.Models.PromotionModel
{
    public class Promotion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int isActive {  get; set; }
        public int Discount {  get; set; }
        public ICollection<Order.Order> ApplyOrders { get; set; }

        public Promotion()
        {
            ApplyOrders = new List<Order.Order>();
        }
    }
}
