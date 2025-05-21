using CafeManagement.Interfaces;

namespace CafeManagement.Models.OrderModel
{
    public class Customer : ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int NumberOfOrders { get; set; }
        public ICollection<Order> Orders { get; set; }
        public bool IsDeleted { get; set; }
        public Customer()
        {
            Orders = new List<Order>();
            IsDeleted = false;
            NumberOfOrders = 0;
        }
    }
}
