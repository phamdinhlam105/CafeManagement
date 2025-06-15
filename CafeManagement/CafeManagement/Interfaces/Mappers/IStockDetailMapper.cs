using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockDetailMapper:IEntityToResponse<StockReportDetail,StockDetailResponse>
    {
    }
}
