using CafeManagement.Dtos.Request.ProductReq;
using CafeManagement.Dtos.Response.ProductRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.ProductModel;

namespace CafeManagement.Mappers
{
    public class RecipeDetailMapper : IRecipeDetailMapper
    {
        public RecipeDetail MapToEntity(RecipeDetailReq req)
        {
            return new RecipeDetail
            {
                IngredientId = req.IngredientId,
                Amount=req.Amount
            };
        }

        public RecipeDetailRes MapToResponse(RecipeDetail entity)
        {
            return new RecipeDetailRes
            {
                Id = entity.Id,
                Amount = entity.Amount,
                IngredientId = entity.IngredientId,
                IngredientName = entity.Ingredient.Name
            };
        }
    }
}
