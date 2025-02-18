using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class ReportCreationService : IReportCreationService
    {
        private IUnitOfWork _unitOfWork;
        private IOrderService _orderService;
        private IStockEntryService _stockEntryService;

        public ReportCreationService(IUnitOfWork unitOfWork, IOrderService orderService, IStockEntryService stockEntryService)
        {
            _unitOfWork = unitOfWork;
            _orderService = orderService;
            _stockEntryService = stockEntryService;
        }
        public void CreateDailyReport(DateOnly date)
        {
            DailyReport dailyReport = new DailyReport();
        }

        public void CreateMonthlyReport(int month, int year)
        {
            MonthlyReport monthlyReport = new MonthlyReport();
        }

        public void CreateQuarterlyReport(int quarter, int year)
        {
            QuarterlyReport quarterlyReport = new QuarterlyReport();
        }
    }
}
