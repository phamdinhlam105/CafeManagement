using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Stock;
using CafeManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockEntryController : ControllerBase
    {
        private IStockEntryService _stockEntryService;
        public StockEntryController(IStockEntryService stockEntryService)
        {
            _stockEntryService = stockEntryService;
        }

        [HttpPost]
        public IActionResult ImportStock([FromBody] StockEntry stockEntry)
        {
            if(ModelState.IsValid)
                return BadRequest(ModelState);
            _stockEntryService.StockImport(stockEntry);
            return RedirectToAction("GetStockRemain","Stock");
        }

        [HttpGet]
        public IActionResult GetAllStockEntry()
        {
            return Ok(_stockEntryService.GetAll());
        }
        [HttpGet("/bydate")]
        public IActionResult GetStockEntryByDate(DateOnly date)
        {
            return Ok(_stockEntryService.GetByDate(date));
        }
    }
}
