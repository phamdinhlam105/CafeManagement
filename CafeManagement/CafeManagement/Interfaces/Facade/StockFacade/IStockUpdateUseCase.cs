using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Facade.StockFacade
{
    public interface IStockUpdateUseCase
    {
        Task NewAdjustment(StockAdjustmentRequest adjustment);
        Task StockImport(StockEntryRequest entry);
    }
}
