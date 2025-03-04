using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class YearlyReportService : IYearlyReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportQueryService _reportQueryService;

        public YearlyReportService(IUnitOfWork unitOfWork, IReportQueryService reportQueryService)
        {
            _unitOfWork = unitOfWork;
            _reportQueryService = reportQueryService;
        }
        public async Task<YearlyReport> CreateYearlyReport(int year)
        {
            var quarterlyReports = (await _unitOfWork.QuarterlyReport.GetAll())
                .Where(qr => qr.createDate.Year == year)  
                .ToList();
            if (quarterlyReports == null || !quarterlyReports.Any())
            {
                throw new Exception($"Không có báo cáo quý nào cho năm {year}.");
            }
            DateTime startYear = new DateTime(year, 1, 1, 0, 0, 0);
            DateTime endYear = new DateTime(year, 12, 31, 23, 59, 59);
            List<Customer> topLoyalCustomers = new List<Customer>();
            YearlyReport yearlyReport = new YearlyReport
            {
                Id = Guid.NewGuid(),
                Year = year,
                TotalRevenue = quarterlyReports.Sum(q => q.TotalRevenue),
                TotalExpenditure = quarterlyReports.Sum(q => q.TotalExpenditure),
                TotalProductsSold = quarterlyReports.Sum(q => q.TotalProductsSold),
                NumberOfFinishedOrders = quarterlyReports.Sum(q => q.NumberOfFinishedOrders),
                NumberOfCancelledOrders = quarterlyReports.Sum(q => q.NumberOfCancelledOrders),
                TopSelling = await _reportQueryService.GetTopSellingProduct(startYear, endYear),
                LeastSelling = await _reportQueryService.GetLeastSellingProduct(startYear, endYear),
                TopLoyalCustomers = topLoyalCustomers,
                QuarterlyReports = quarterlyReports,
                createDate = DateTime.Now
            };
            await _unitOfWork.YearlyReport.Add(yearlyReport);
            return yearlyReport;
        }

        public async Task<IEnumerable<YearlyReport>> GetAllYearlyReports()
        {
            return (await _unitOfWork.YearlyReport.GetAll()).ToList();
        }

        public async Task<YearlyReport> GetYearlyReport(int year)
        {
            return (await _unitOfWork.YearlyReport.GetAll())
               .FirstOrDefault(yr => yr.Year == year);
        }
    }
}
