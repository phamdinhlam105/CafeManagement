using CafeManagement.Dtos.Request.ProductReq;
using CafeManagement.Dtos.Response.ProductRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.ProductModel;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IRecipeDetailMapper:IRequestToEntity<RecipeDetailReq,RecipeDetail>,
        IEntityToResponse<RecipeDetail,RecipeDetailRes>
    {

    }
}
