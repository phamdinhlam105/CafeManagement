using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class ReportCreationService : IReportCreationService
    {
        private IUnitOfWork _unitOfWork;

        public ReportCreationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
