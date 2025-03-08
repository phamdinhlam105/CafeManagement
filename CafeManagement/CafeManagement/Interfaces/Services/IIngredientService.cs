using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services
{
    public interface IIngredientService
    {
        Task<Ingredient> CreateIngredient(Ingredient ingredient);
        Task<Ingredient> UpdateIngredient(Guid Id, Ingredient ingredient);
        Task DeleteIngredient(Ingredient ingredient);
        Task <IEnumerable<Ingredient>> GetAll();
    }
}
