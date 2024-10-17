namespace CafeManagement.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int No {  get; set; }
        public double Price { get; set; }
        public int Quantity {  get; set; }
        public DateTime createdAt {  get; set; }
        public ICollection<OrderDetail> Details { get; set;}

        public Order()
        {
            Details = new List<OrderDetail>();
        }
    }
}
