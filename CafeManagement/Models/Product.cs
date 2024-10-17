namespace CafeManagement.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

        public Product()
        {
            Details = new List<OrderDetail>();
        }
    }
}
