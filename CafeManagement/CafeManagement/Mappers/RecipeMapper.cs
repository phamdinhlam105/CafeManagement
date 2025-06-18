using CafeManagement.Dtos.Request.ProductReq;
using CafeManagement.Dtos.Response.ProductRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.ProductModel;

namespace CafeManagement.Mappers
{
    public class RecipeMapper : IRecipeMapper
    {
        private readonly IRecipeDetailMapper _recipeDetailMapper;
        public RecipeMapper(IRecipeDetailMapper recipeDetailMapper)
        {
            _recipeDetailMapper = recipeDetailMapper;
        }

        public Recipe MapToEntity(RecipeReq req)
        {
            return new Recipe
            {
                ProductId = req.ProductId,
                Details = req.Details.Select(d => _recipeDetailMapper.MapToEntity(d)).ToList()
            };
        }

        public RecipeRes MapToResponse(Recipe entity)
        {
            return new RecipeRes
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Details = entity.Details.Select(d => _recipeDetailMapper.MapToResponse(d)).ToList()
            };
        }
    }
}
