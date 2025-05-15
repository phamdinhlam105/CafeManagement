namespace CafeManagement.Models.Stock
{
    public class Recipe
    {
        public Guid Id {  get; set; }
        public Guid ProductId {  get; set; }
        public decimal TotalCost {  get; set; }
        public Product Product { get; set; }
        public ICollection<RecipeDetail> Details { get; set; }
        public Recipe()
        {
            Details= new List<RecipeDetail>();
        }
    }
}
