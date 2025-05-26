
using CafeManagement.Models.OrderModel;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportUpdateService
    {
        Task UpdateOrderReport(Order order);
        Task UpdateStockReport(StockEntry stockEntry);
        Task UpdateProductReport(DailyReport todayReport);
    }
}
