using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Stock
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        async Task IIngredientService.createIngredient(Ingredient ingredient)
        {
            await _unitOfWork.Ingredient.Add(ingredient);
        }

        async Task IIngredientService.deleteIngredient(Ingredient ingredient)
        {
            await _unitOfWork.Ingredient.Delete(ingredient);
        }

        async Task IIngredientService.updateIngredient(Ingredient ingredient)
        {
            await _unitOfWork.Ingredient.Update(ingredient);
        }
    }
}
