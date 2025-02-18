using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services
{
    public interface IIngredientService
    {
        void createIngredient(Ingredient ingredient);
        void updateIngredient(Ingredient ingredient);
        void deleteIngredient(Ingredient ingredient);
    }
}
