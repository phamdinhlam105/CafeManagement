namespace CafeManagement.Dtos.Response.ProductRes
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfProducts {  get; set; }
    }
}
