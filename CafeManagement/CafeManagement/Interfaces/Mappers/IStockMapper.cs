using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Models;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockMapper
    {
        StockResponse MapToResponse(DailyStock stock);
    }
}
