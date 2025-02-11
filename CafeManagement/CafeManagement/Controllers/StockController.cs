using CafeManagement.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;
        private IStockEntryService _stockEntryService;
        public StockController(IStockService stockService, IStockEntryService stockEntryService)
        {
            _stockService = stockService;
            _stockEntryService = stockEntryService;
        }
        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(_stockService.GetAllDailyStocks());
        }
        [HttpGet("/bydate")]
        public IActionResult GetDetailByDate(DateTime date)
        {
            return Ok(_stockService.GetDetailByDate(date));
        }
        [HttpGet("/remain")]
        public IActionResult GetStockRemain()
        {
            return Ok(_stockService.StockRemain());
        }
    }
}
