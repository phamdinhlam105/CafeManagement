using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IStockMapper : IEntityToResponse<DailyStock, StockResponse>
    {
    }
}
