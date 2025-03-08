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
        public async Task<Ingredient> CreateIngredient(Ingredient ingredient)
        {
            if (ingredient.Id == Guid.Empty)
                ingredient.Id = Guid.NewGuid();
            await _unitOfWork.Ingredient.Add(ingredient);
            return ingredient;
        }

        public async Task DeleteIngredient(Ingredient ingredient)
        {
            await _unitOfWork.Ingredient.Delete(ingredient);
        }

        public async Task<Ingredient> UpdateIngredient(Guid Id,Ingredient ingredient)
        {
            var oldIngredient = await _unitOfWork.Ingredient.GetById(Id);
            if (oldIngredient == null)
                return null;
            if (ingredient.Id == Guid.Empty)
                ingredient.Id = Id;
            await _unitOfWork.Ingredient.Update(ingredient);
            return ingredient;

        }
        public async Task<IEnumerable<Ingredient>> GetAll()
        {
            return await _unitOfWork.Ingredient.GetAll();
        }
    }
}
