using CafeManagement.Interfaces;

namespace CafeManagement.Models
{
    public class Category:ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
        public bool IsDeleted {  get; set; }
        public Category()
        {
            Products = new List<Product>();
            IsDeleted = false;
        }
    }
}
