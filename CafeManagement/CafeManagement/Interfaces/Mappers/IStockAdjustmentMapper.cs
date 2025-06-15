using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Dtos.Response.StockRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockAdjustmentMapper:IRequestToEntity<StockAdjustmentRequest,StockAdjustment>,
        IEntityToResponse<StockAdjustment,StockAdjustmentResponse>
    {
    }
}
