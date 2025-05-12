using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRetrievalService _reportRetrievalService;
        private readonly IYearlyReportService _yearlyReportService;
        private readonly IReportUpdateService _reportUpdateService;
        public ReportController(IReportRetrievalService reportRetrievalService, IYearlyReportService yearlyReportService,IReportUpdateService reportUpdateService)
        {
            _reportRetrievalService = reportRetrievalService;
            _yearlyReportService = yearlyReportService;
            _reportUpdateService = reportUpdateService;
        }

        [HttpGet("daily")] 
        public async Task<IActionResult> getDailyReport(DateOnly date) 
        {
            if (date > Ultilities.GetToday())
            {
                return BadRequest("Invalid date");
            }
            try
            {
                return Ok(await _reportRetrievalService.GetDailyReport(date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("daily/range")]
        public async Task<IActionResult> GetDailyReportByRange(DateOnly startDate,DateOnly endDate)
        {
            if (startDate > Ultilities.GetToday())
            {
                return BadRequest("Invalid date");
            }
            try
            {
                return Ok(await _reportRetrievalService.GetReportsByRange(startDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("monthly")]
        public async Task<IActionResult> getMonthlyReport(int month, int year)
        {
            DateOnly startDate = new DateOnly(year, month, 1); 

            if (startDate > Ultilities.GetToday())
            {
                return BadRequest("Invalid date");
            }
            try
            {
                return Ok(await _reportRetrievalService.GetMonthlyReport(month, year));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("quarterly")]
        public async Task<IActionResult> getQuarterlyReport(int quarter, int year)
        {
            DateOnly today = Ultilities.GetToday();
            var (startDate, _) = QuarterHelper.GetQuarterDates(year, quarter);

            if (startDate > today)
            {
                return BadRequest("Invalid date");
            }
            try
            {
                return Ok(await _reportRetrievalService.GetQuarterlyReport(quarter, year));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
     
        [HttpPost("daily")]
        public async Task<IActionResult> UpdateDailyReport(DateOnly date)
        {
            if (date > Ultilities.GetToday())
                return BadRequest("Invalid date");
            try
            {
                await _reportUpdateService.UpdateDailyReport(date);
            }
            catch(Exception err)
            {
                return BadRequest(err.Message);
            }
            return Ok();
        }
        [HttpPost("monthly")]
        public async Task<IActionResult> UpdateMonthlyReport(int month, int year)
        {
            DateOnly today = Ultilities.GetToday();
            var (startDate, _) = QuarterHelper.GetQuarterDates(year, month);

            if (startDate > today)
            {
                return BadRequest("Invalid date");
            }
            try
            {
                await _reportUpdateService.UpdateMonthlyReport(month,year);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            return Ok();
        }
        [HttpPost("quarterly")]
        public async Task<IActionResult> UpdateQuarterlyReport(int quarter, int year)
        {
            DateOnly today = Ultilities.GetToday();
            var (startDate, _) = QuarterHelper.GetQuarterDates(year, quarter);

            if (startDate > today)
            {
                return BadRequest("Invalid date");
            }
            try
            {
                await _reportUpdateService.UpdateQuarterlyReport(quarter,year);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            return Ok();
        }
    }
}
