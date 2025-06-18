using CafeManagement.Models.ProductModel;

namespace CafeManagement.Dtos.Request.ProductReq
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Img {  get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public List<RecipeDetail> Recipes { get; set; }
        public ProductRequest()
        {
            Recipes = new List<RecipeDetail>();
        }
    }
}
