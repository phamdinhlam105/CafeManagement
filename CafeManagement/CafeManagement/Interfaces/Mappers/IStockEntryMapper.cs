using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockEntryMapper
    {
        StockEntryResponse MapToResponse(StockEntry entry);
        StockEntry MaptoEntity(StockEntryRequest request);
    }
}
