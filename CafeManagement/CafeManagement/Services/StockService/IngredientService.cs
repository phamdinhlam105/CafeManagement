using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Ingredient> Add(Ingredient ingredient)
        {
            if (ingredient.Id == Guid.Empty)
                ingredient.Id = Guid.NewGuid();
            await _unitOfWork.Ingredient.Add(ingredient);
            return ingredient;
        }

        public async Task Delete(Ingredient ingredient)
        {
            await _unitOfWork.Ingredient.Delete(ingredient);
        }

        public async Task<Ingredient> Update(Ingredient ingredient)
        {
            var oldIngredient = await _unitOfWork.Ingredient.GetById(ingredient.Id);
            if (oldIngredient == null)
                return null;
            if (ingredient.Id == Guid.Empty)
                ingredient.Id = ingredient.Id;
            await _unitOfWork.Ingredient.Update(ingredient);
            return ingredient;

        }
        public async Task<Ingredient> GetById(Guid Id)
        {
            return await _unitOfWork.Ingredient.GetById(Id);
        }

        public async Task<IEnumerable<Ingredient>> GetAll()
        {
            return await _unitOfWork.Ingredient.GetAll();
        }
    }
}
