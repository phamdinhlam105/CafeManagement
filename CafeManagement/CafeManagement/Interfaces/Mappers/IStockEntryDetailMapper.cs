
using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockEntryDetailMapper:IRequestToEntity<StockEntryDetailRequest,StockEntryDetail>,
        IEntityToResponse<StockEntryDetail,StockEntryDetailResponse>
    {
    }
}
