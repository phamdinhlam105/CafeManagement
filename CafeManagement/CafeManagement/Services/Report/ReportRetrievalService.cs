using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CafeManagement.Services.Report
{
    public class ReportRetrievalService : IReportRetrievalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportQueryService _reportQueryService;
        public ReportRetrievalService(IUnitOfWork unitOfWork , IReportQueryService reportQueryService)
        {
            _unitOfWork = unitOfWork;
            _reportQueryService = reportQueryService;
        }
        public async Task<DailyReport?> GetDailyReport(DateOnly date)
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(date);
            
            if(!todayReport.IsOrderReportUpToDate)
            {
                todayReport.OrderReport.TopSelling = todayReport.ProductReports.OrderByDescending(pr => pr.QuantitySold).FirstOrDefault().Product;
                todayReport.OrderReport.LeastSelling = todayReport.ProductReports.OrderBy(pr => pr.QuantitySold).FirstOrDefault().Product;
                todayReport.IsOrderReportUpToDate = true;
                await _unitOfWork.DailyReport.Update(todayReport);
            }
            return todayReport;
        }

        public async Task<ReportResponse?> GetMonthlyReport(int month, int year)
        {
            return null;
        }

        public async Task<ReportResponse?> GetQuarterlyReport(int quarter, int year)
        {
            return null;
        }

        public async Task<List<DailyReport>> GetReportsByRange(DateOnly startDate, DateOnly endDate)
        {
            var dailyReportList = (await _unitOfWork.DailyReport.GetByDateRange(startDate, endDate)).ToList();
            foreach (var dailyReport in dailyReportList)
            {
                if (!dailyReport.IsOrderReportUpToDate)
                {
                    dailyReport.OrderReport.TopSelling = dailyReport.ProductReports.OrderByDescending(pr => pr.QuantitySold).FirstOrDefault().Product;
                    dailyReport.OrderReport.LeastSelling = dailyReport.ProductReports.OrderBy(pr => pr.QuantitySold).FirstOrDefault().Product;
                    dailyReport.IsOrderReportUpToDate = true;
                    await _unitOfWork.DailyReport.Update(dailyReport);
                }
            }
            return dailyReportList;
        }
    }
}
