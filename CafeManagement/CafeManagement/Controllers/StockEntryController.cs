using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockEntryController : ControllerBase
    {
        private readonly IStockEntryService _stockEntryService;
        private readonly IStockEntryMapper _stockEntryMapper;
        public StockEntryController(IStockEntryService stockEntryService,IStockEntryMapper stockEntryMapper)
        {
            _stockEntryService = stockEntryService;
            _stockEntryMapper = stockEntryMapper;
        }

        [HttpPost]
        public async Task<IActionResult> ImportStock([FromBody] StockEntryRequest stockEntry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _stockEntryService.StockImport(_stockEntryMapper.MapToEntity(stockEntry));
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStockEntry()
        {
            return Ok((await _stockEntryService.GetAll()).Select(se=>_stockEntryMapper.MapToResponse(se)));
        }
        [HttpGet("bydate/stockentry")]
        public async Task<IActionResult> GetStockEntryByDate(DateOnly date)
        {
            return Ok((await _stockEntryService.GetByDate(date)).Select(se=>_stockEntryMapper.MapToResponse(se)));
        }

        [HttpGet("bydate")]
        public async Task<IActionResult> GetStockEntriesByDate(DateOnly date)
        {
            return Ok((await _stockEntryService.GetByDate(date)).Select(se => _stockEntryMapper.MapToResponse(se)));
        }
    }
}
