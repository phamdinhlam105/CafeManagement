namespace CafeManagement.Dtos.Request.ProductReq
{
    public class RecipeReq
    {
        public Guid ProductId {  get; set; }
        public List<RecipeDetailReq> Details { get; set; }
        public RecipeReq()
        {
            Details = new List<RecipeDetailReq> { };
        }
    }
}
