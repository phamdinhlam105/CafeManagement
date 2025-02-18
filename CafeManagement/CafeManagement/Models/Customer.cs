

namespace CafeManagement.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address {  get; set; }

        public ICollection<Order.Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order.Order>();
        }
    }
}
