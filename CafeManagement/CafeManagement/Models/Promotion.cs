namespace CafeManagement.Models
{
    public class Promotion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<Order.Order> ApplyOrders {  get; set; }

        public Promotion()
        {
            ApplyOrders = new List<Order.Order>();
        }
    }
}
