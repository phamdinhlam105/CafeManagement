namespace CafeManagement.Models.ProductModel
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<RecipeDetail> Details { get; set; }
        public Recipe()
        {
            Details = new List<RecipeDetail>();
        }
    }
}
