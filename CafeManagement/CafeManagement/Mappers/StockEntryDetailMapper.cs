using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Stock;

namespace CafeManagement.Mappers
{
    public class StockEntryDetailMapper : IStockEntryDetailMapper
    {
        public StockEntryDetail MapToEntity(StockEntryDetailRequest req)
        {
            return new StockEntryDetail
            {
                Id = Guid.NewGuid(),
                IngredientId = req.IngredientId,
                Quantity = req.Quantity,
                Price = req.Price
            };
        }

        public StockEntryDetailResponse MapToResponse(StockEntryDetail entity)
        {
            return new StockEntryDetailResponse
            {
                Id = entity.Id,
                Quantity = entity.Quantity,
                Price = entity.Price,
                Ingredient = entity.Ingredient,
                IngredientId = entity.IngredientId,
                StockEntryId = entity.StockEntryId
            };
        }
    }
}
