using CafeManagement.Models;
using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportQueryService
    {
        Task<decimal> GetTotalRevenue(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalExpenditure(DateTime startDate, DateTime endDate);
        Task<int> GetNumberOfFinishedOrders(DateTime startDate, DateTime endDate);
        Task<int> GetNumberOfCancelledOrders(DateTime startDate, DateTime endDate);
        Task<Product> GetTopSellingProduct(DateTime startDate, DateTime endDate);
        Task<Product> GetLeastSellingProduct(DateTime startDate, DateTime endDate);
        Task<List<int>> GetPeakHours(DateOnly date);
        Task<IEnumerable<BestDays>> GetBestDaysInWeek(DateTime startDate, DateTime endDate);
        Task<int> GetTotalProductsSold(DateTime startTime, DateTime endTime);
    }
}
