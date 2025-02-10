namespace CafeManagement.Dtos.Respone
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
