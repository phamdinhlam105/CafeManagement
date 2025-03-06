using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockEntryController : ControllerBase
    {
        private readonly IStockEntryService _stockEntryService;
        public StockEntryController(IStockEntryService stockEntryService)
        {
            _stockEntryService = stockEntryService;
        }

        [HttpPost]
        public async Task<IActionResult> ImportStock([FromBody] StockEntry stockEntry)
        {
            if(ModelState.IsValid)
                return BadRequest(ModelState);
            await _stockEntryService.StockImport(stockEntry);
            return RedirectToAction("GetStockRemain","Stock");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStockEntry()
        {
            return Ok(await _stockEntryService.GetAll());
        }
        [HttpGet("/bydate/stockentry")]
        public async Task<IActionResult> GetStockEntryByDate(DateOnly date)
        {
            return Ok(await _stockEntryService.GetByDate(date));
        }
    }
}
