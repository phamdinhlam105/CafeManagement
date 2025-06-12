using CafeManagement.Interfaces.Services.ProductService;
using CafeManagement.Models.ProductModel;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.ProductService
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWOrk;
        public RecipeService(IUnitOfWork unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
        }

        public async Task Edit(Guid productId, Recipe newRecipe)
        {
            var currentRecipe = await _unitOfWOrk.Recipe.GetByProductId(productId);
            if (currentRecipe == null)
                throw new Exception("Product Id not found");
            var currentDetails = currentRecipe.Details.ToDictionary(d => d.IngredientId);
            var newDetails = newRecipe.Details.ToDictionary(d => d.IngredientId);
            foreach (var newDetail in newRecipe.Details)
            {
                if (currentDetails.TryGetValue(newDetail.IngredientId, out var existingDetail))
                    existingDetail.Amount = newDetail.Amount;
                else
                {
                    currentRecipe.Details.Add(new RecipeDetail
                    {
                        Id = new Guid(),
                        IngredientId = newDetail.IngredientId,
                        Amount = newDetail.Amount,
                    });
                }
            }
            foreach(var currentDetail in currentRecipe.Details.ToList())
            {
                if (!newDetails.TryGetValue(currentDetail.IngredientId, out var existingDetail))
                {
                    await _unitOfWOrk.RecipeDetail.Delete(currentDetail);
                    currentRecipe.Details.Remove(currentDetail);
                }
                continue;
            }
            await _unitOfWOrk.Recipe.Update(currentRecipe);
        }
    }
}
