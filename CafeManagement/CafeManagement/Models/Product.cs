using CafeManagement.Models;
using CafeManagement.Models.Order;

namespace CafeManagement.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Img {  get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

        public Product()
        {
            Details = new List<OrderDetail>();
        }
    }
}
