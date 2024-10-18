namespace CafeManagement.Dtos.Request
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
