using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        void IIngredientService.createIngredient(Ingredient ingredient)
        {
            _unitOfWork.Ingredient.Add(ingredient);
        }

        void IIngredientService.deleteIngredient(Ingredient ingredient)
        {
            _unitOfWork.Ingredient.Delete(ingredient);
        }

        void IIngredientService.updateIngredient(Ingredient ingredient)
        {
            _unitOfWork.Ingredient.Update(ingredient);
        }
    }
}
