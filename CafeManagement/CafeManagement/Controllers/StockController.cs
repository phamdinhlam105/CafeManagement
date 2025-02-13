using CafeManagement.Dtos.Request;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet]
        public IActionResult GetAll() 
        {
            return Ok(_stockService.GetAllDailyStocks());
        }
        [HttpGet("/bydate")]
        public IActionResult GetDetailByDate(DateOnly date)
        {
            return Ok(_stockService.GetDetailByDate(date));
        }
        [HttpGet("/remain")]
        public IActionResult GetStockRemain()
        {
            return Ok(_stockService.StockRemain());
        }
        [HttpPost("/update")]
        public IActionResult UpdateRemainStock([FromBody]UpdateStockRequest updatedStock)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            foreach (var detail in updatedStock.Details)
            {
                _stockService.StockUpdate(updatedStock.Id, detail.Ingredient, detail.remainAmount);
            }
            return Ok();
        }

    }
}
