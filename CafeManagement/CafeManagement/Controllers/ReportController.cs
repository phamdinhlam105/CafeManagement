using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Roles = Role.Manager)]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportCreationService _reportCreationService;
        private readonly IReportRetrievalService _reportRetrievalService;
        private readonly IYearlyReportService _yearlyReportService;
        public ReportController(IReportCreationService reportCreationService, IReportRetrievalService reportRetrievalService, IYearlyReportService yearlyReportService)
        {
            _reportCreationService = reportCreationService;
            _reportRetrievalService = reportRetrievalService;
            _yearlyReportService = yearlyReportService;
        }

        [HttpPost]
        public async Task<IActionResult> createReport(DateTime date)
        {
            await _reportCreationService.CreateDailyReport(date);
            return Ok(await _reportRetrievalService.GetDailyReport(DateOnly.FromDateTime(date)));
        }
        [HttpGet("daily")] 
        public async Task<IActionResult> getDailyReport(DateOnly date) 
        {
            return Ok(await _reportRetrievalService.GetDailyReport(date));
        }
        [HttpGet("monthly")]
        public async Task<IActionResult> getMonthlyReport(int month, int year)
        {
            return Ok(await _reportRetrievalService.GetMonthlyReport(month, year));
        }
        [HttpGet("quarterly")]
        public async Task<IActionResult> getQuarterlyReport(int quarter, int year)
        {
            return Ok(await _reportRetrievalService.GetQuarterlyReport(quarter, year));
        }
        [HttpGet("yearly/GetByYear")]
        public async Task<IActionResult> getYearlyReport(int year)
        {
            return Ok(await _yearlyReportService.GetYearlyReport(year));
        }
        [HttpGet("yearly")]
        public async Task<IActionResult> getAllYearlyReports()
        {
            return Ok(await _yearlyReportService.GetAllYearlyReports());
        }
    }
}
