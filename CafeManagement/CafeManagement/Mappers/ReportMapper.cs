using CafeManagement.Dtos.Respone.ReportRes;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Report;

namespace CafeManagement.Mappers
{
    public class ReportMapper : IReportMapper
    {
        public OneDayReportResponse MapToResponse(DailyReport entity)
        {
            return new OneDayReportResponse
            {
                TotalRevenue = entity.OrderReport.TotalRevenue,
                TopSellingProduct = entity.OrderReport.TopSelling?.Name,
                LeastSellingProduct = entity.OrderReport.LeastSelling?.Name,
                NumberOfFinishedOrders = entity.OrderReport.NumberOfFinishedOrders,
                NumberOfCancelledOrders = entity.OrderReport.NumberOfCancelledOrders,
                TotalProductsSold = entity.OrderReport.TotalProductsSold,
                ReportDate = entity.ReportDate,
                TotalExpenditure = entity.StockReport.TotalExpenditure,
                NumberOfImports = entity.StockReport.NumberOfImports
            };
        }
    }
}
