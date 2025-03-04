using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services
{
    public interface IIngredientService
    {
        Task createIngredient(Ingredient ingredient);
        Task updateIngredient(Ingredient ingredient);
        Task deleteIngredient(Ingredient ingredient);
    }
}
