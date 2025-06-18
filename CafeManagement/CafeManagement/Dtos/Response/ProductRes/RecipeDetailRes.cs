namespace CafeManagement.Dtos.Response.ProductRes
{
    public class RecipeDetailRes
    {
        public Guid Id {  get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public float Amount { get; set; }
    }
}
