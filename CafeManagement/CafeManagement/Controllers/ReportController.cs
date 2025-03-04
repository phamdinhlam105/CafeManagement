using CafeManagement.Interfaces.Services.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportCreationService _reportCreationService;
        private IReportRetrievalService _reportRetrievalService;
        private IYearlyReportService _yearlyReportService;
        public ReportController(IReportCreationService reportCreationService, IReportRetrievalService reportRetrievalService, IYearlyReportService yearlyReportService)
        {
            _reportCreationService = reportCreationService;
            _reportRetrievalService = reportRetrievalService;
            _yearlyReportService = yearlyReportService;
        }

        [HttpPost]
        public IActionResult createReport(DateTime date)
        {
            _reportCreationService.CreateDailyReport(date);
            return Ok(_reportRetrievalService.GetDailyReport(DateOnly.FromDateTime(date)));
        }
        [HttpGet("daily")] 
        public IActionResult getDailyReport(DateOnly date) 
        {
            return Ok(_reportRetrievalService.GetDailyReport(date));
        }
        [HttpGet("monthly")]
        public IActionResult getMonthlyReport(int month, int year)
        {
            return Ok(_reportRetrievalService.GetMonthlyReport(month,year));
        }
        [HttpGet("quarterly")]
        public IActionResult getQuarterlyReport(int quarter, int year)
        {
            return Ok(_reportRetrievalService.GetQuarterlyReport(quarter, year));
        }
        [HttpGet("yearly/GetByYear")]
        public IActionResult getYearlyReport(int year)
        {
            return Ok(_yearlyReportService.GetYearlyReport(year));
        }
        [HttpGet("yearly")]
        public IActionResult getAllYearlyReports()
        {
            return Ok(_yearlyReportService.GetAllYearlyReports());
        }
    }
}
