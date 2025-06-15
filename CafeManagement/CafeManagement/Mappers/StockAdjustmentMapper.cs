using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Dtos.Response.StockRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Stock;

namespace CafeManagement.Mappers
{
    public class StockAdjustmentMapper : IStockAdjustmentMapper
    {
        public StockAdjustment MapToEntity(StockAdjustmentRequest req)
        {
            return new StockAdjustment
            {
                Reason = req.Reason,
                AdjustedBy = req.AdjustedBy,
                Notes = req.Notes,
                AdjustmentDetails = req.Details.Select(d => new AdjustmentDetail
                {
                    QuantityAdjusted = d.QuantityAdjusted,
                    IngredientId = d.IngredientId
                })
            };
        }

        public StockAdjustmentResponse MapToResponse(StockAdjustment entity)
        {
            return new StockAdjustmentResponse
            {
                Id = entity.Id,
                Reason = entity.Reason,
                AdjustedBy = entity.AdjustedBy,
                CreatedAt = entity.AdjustmentDate,
                Details = entity.AdjustmentDetails.Select(ad => new AdjustmentDetailResponse
                {
                    Id = ad.Id,
                    StockAdjustmentId = ad.StockAdjustmentId,
                    AdjustValue = ad.AdjustValue,
                    QuantityAdjusted = ad.QuantityAdjusted,
                    IngredientId = ad.IngredientId,
                    IngredientName = ad.Ingredient.Name
                }).ToList()
            };
        }
    }
}
