using CafeManagement.Models.ProductModel;

namespace CafeManagement.Interfaces.Services.ProductService
{
    public interface IRecipeService
    {
        Task Edit(Guid productId, Recipe newRecipe);
    }
}
