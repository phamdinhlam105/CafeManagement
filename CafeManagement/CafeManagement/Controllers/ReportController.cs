﻿using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
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
        private readonly IReportMapper _reportMapper;
        public ReportController(IReportRetrievalService reportRetrievalService,IReportMapper reportMapper)
        {
            _reportRetrievalService = reportRetrievalService;
            _reportMapper = reportMapper;
        }

        [HttpGet("daily")] 
        public async Task<IActionResult> GetDailyReport(DateOnly date) 
        {
            if (date > Ultilities.GetToday())
            {
                return BadRequest("Invalid date");
            }
            try
            {
                var dailyReport = await _reportRetrievalService.GetDailyReport(date);
                if (dailyReport == null)
                    return NoContent();
                return Ok(_reportMapper.MapToResponse(dailyReport));
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
                var listReport = await _reportRetrievalService.GetReportsByRange(startDate, endDate);
                return Ok(listReport.Select(dr=>_reportMapper.MapToResponse(dr)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport(int month, int year)
        {
            DateOnly startDate = new(year, month, 1); 

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
        public async Task<IActionResult> GetQuarterlyReport(int quarter, int year)
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
        }*/
    }
}
