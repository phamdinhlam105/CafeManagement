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
        private readonly IReportRetrievalService _reportRetrievalService;
        private readonly IYearlyReportService _yearlyReportService;
        public ReportController(IReportRetrievalService reportRetrievalService, IYearlyReportService yearlyReportService)
        {
            _reportRetrievalService = reportRetrievalService;
            _yearlyReportService = yearlyReportService;
        }

        [HttpGet("daily")] 
        public async Task<IActionResult> getDailyReport(DateOnly date) 
        {
            if (date > Ultilities.GetToday())
            {
                return BadRequest("Ngày báo cáo không thể ở tương lai.");
            }
            return Ok(await _reportRetrievalService.GetDailyReport(date));
        }
        [HttpGet("monthly")]
        public async Task<IActionResult> getMonthlyReport(int month, int year)
        {
            DateOnly startDate = new DateOnly(year, month, 1); 

            if (startDate > Ultilities.GetToday())
            {
                return BadRequest("Tháng báo cáo không thể ở tương lai.");
            }
            return Ok(await _reportRetrievalService.GetMonthlyReport(month, year));
        }
        [HttpGet("quarterly")]
        public async Task<IActionResult> getQuarterlyReport(int quarter, int year)
        {
            DateOnly today = Ultilities.GetToday();
            var (startDate, _) = QuarterHelper.GetQuarterDates(year, quarter);

            if (startDate > today)
            {
                return BadRequest("Quý báo cáo không thể ở tương lai.");
            }
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
