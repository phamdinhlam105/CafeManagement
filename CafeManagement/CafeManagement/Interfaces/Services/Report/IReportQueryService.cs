using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services.Report
{
    public interface IReportQueryService
    {
        decimal GetTotalRevenue(DateTime startDate, DateTime endDate);
        decimal GetTotalExpenditure(DateTime startDate, DateTime endDate);
        int GetNumberOfFinishedOrders(DateTime startDate, DateTime endDate);
        int GetNumberOfCancelledOrders(DateTime startDate, DateTime endDate);
        Product GetTopSellingProduct(DateTime startDate, DateTime endDate);
        Product GetLeastSellingProduct(DateTime startDate, DateTime endDate);
        List<int> GetPeakHours(DateTime date);
        DateOnly GetBestDayInWeek(int month, int year);
    }
}
