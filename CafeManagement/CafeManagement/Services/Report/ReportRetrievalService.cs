using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CafeManagement.Services.Report
{
    public class ReportRetrievalService : IReportRetrievalService
    {
        private IUnitOfWork _unitOfWork;
        public ReportRetrievalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public DailyReport? GetDailyReport(DateOnly date)
        {
            return _unitOfWork.DailyReport.GetAll()
               .FirstOrDefault(dr => dr.createDate.Equals(date));
        }

        public MonthlyReport? GetMonthlyReport(int month, int year)
        {
            return _unitOfWork.MonthlyReport.GetAll()
            .FirstOrDefault(mr => mr.StartDate.Year == year && mr.StartDate.Month == month);
        }

        public QuarterlyReport? GetQuarterlyReport(int quarter, int year)
        {
            var (startDateTime, endDateTime) = QuarterHelper.GetQuarterDates(year, quarter);
            var startDate = DateOnly.FromDateTime(startDateTime);
            var endDate = DateOnly.FromDateTime(endDateTime);
            return _unitOfWork.QuarterlyReport.GetAll()
                .FirstOrDefault(qr => qr.StartDate >= startDate && qr.EndDate <= endDate);
        }

        public IEnumerable<DailyReport> GetReportsByRange(DateOnly startDate, DateOnly endDate)
        {
            return _unitOfWork.DailyReport.GetAll()
               .Where(dr => dr.createDate.CompareTo(startDate) >= 0 && dr.createDate.CompareTo(endDate) <= 0)
               .ToList();
        }
    }
}
