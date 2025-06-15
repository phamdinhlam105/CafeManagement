using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Report;

namespace CafeManagement.Mappers
{
    public class StockDetailMapper : IStockDetailMapper
    {
        public StockDetailResponse MapToResponse(StockReportDetail entity)
        {
            return new StockDetailResponse
            {
                Id = entity.Id,
                StockUsage = entity.QuantityExported,
                StockImport = entity.QuantityImported,
                StockRemaining = entity.QuantityOnHand,
                DailyStockId = entity.Id,
                IngredientId = entity.IngredientId,
                IngredientName = entity.IngredientName
            };
        }
    }
}
