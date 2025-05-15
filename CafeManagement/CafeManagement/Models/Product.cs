using CafeManagement.Interfaces;
using CafeManagement.Models;
using CafeManagement.Models.Order;
using CafeManagement.Models.Stock;
using Newtonsoft.Json;

namespace CafeManagement.Models
{
    public class Product:ISoftDeletable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Img {  get; set; }
        public string? Description {  get; set; }
        public Guid CategoryId { get; set; }
        public Guid RecipeId {  get; set; }
        public Recipe Recipe { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderDetail> Details { get; set; }

        public bool IsDeleted {  get; set; }

        public Product()
        {
            Details = new List<OrderDetail>();
            IsDeleted = false;
        }
    }
}
