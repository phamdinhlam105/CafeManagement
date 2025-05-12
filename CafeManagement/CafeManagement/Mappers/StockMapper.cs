using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Stock;

namespace CafeManagement.Mappers
{
    public class StockMapper : IStockMapper
    {
        public StockResponse MapToResponse(DailyStock stock)
        {
            return new StockResponse
            {
                Id = stock.Id,
                createDate = stock.createDate,
                Details = stock.DailyStockDetails.Select(dtd => new StockDetailResponse
                {
                    Id = dtd.Id,
                    StockAtStartOfDay = dtd.StockAtStartOfDay,
                    StockImport = dtd.StockImport,
                    StockRemaining = dtd.StockRemaining,
                    DailyStockId = stock.Id,
                    IngredientId = dtd.IngredientId,
                    Ingredient = dtd.Ingredient
                }).ToList()
            };
        }


    }
}
