
using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Dtos.Respone.StockRes;
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
                IngredientId = req.IngredientId,
                ImportQuantity = req.Quantity,
                RemainQuantity = req.Quantity,
                Price = req.Price,
            };
        }

        public StockEntryDetailResponse MapToResponse(StockEntryDetail entity)
        {
            return new StockEntryDetailResponse
            {
                Id = entity.Id,
                ImportQuantity = entity.ImportQuantity,
                RemainQuantity = entity.RemainQuantity,
                Price = entity.Price,
                IngredientId = entity.IngredientId,
                StockEntryId = entity.StockEntryId,
                IngredientName = entity.Ingredient.Name
            };
        }
    }
}
