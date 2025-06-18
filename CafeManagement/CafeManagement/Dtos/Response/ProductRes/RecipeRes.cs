namespace CafeManagement.Dtos.Response.ProductRes
{
    public class RecipeRes
    {
        public Guid Id { get; set; }
        public Guid ProductId {  get; set; }
        public List<RecipeDetailRes> Details { get; set; }
        public RecipeRes()
        {
            Details = new List<RecipeDetailRes>();
        }
    }
}
