namespace CafeManagement.Models.Stock
{
    public class RecipeDetail
    {
        public Guid Id { get; set; }
        public Guid RecipeId {  get; set; }
        public Guid IngredientId {  get; set; }
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient { get; set; }
        public float Amount {  get; set; }
    }
}
