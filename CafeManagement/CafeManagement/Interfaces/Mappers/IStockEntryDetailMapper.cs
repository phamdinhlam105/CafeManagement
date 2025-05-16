using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockEntryDetailMapper:IRequestToEntity<StockEntryDetailRequest,StockEntryDetail>,
        IEntityToResponse<StockEntryDetail,StockEntryDetailResponse>
    {
    }
}
