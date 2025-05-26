using CafeManagement.Models.ProductModel;

namespace CafeManagement.Interfaces.Repositories.ProductRepo
{
    public interface IRecipeRepository:IRepository<Recipe>
    {
        Task<Recipe> GetByProductId(Guid productId);
    }
}
