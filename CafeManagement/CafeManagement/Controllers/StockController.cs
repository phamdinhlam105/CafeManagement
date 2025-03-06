using CafeManagement.Dtos.Request;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _stockService.GetAllDailyStocks());
        }
        [HttpGet("/bydate/stock")]
        public async Task<IActionResult> GetDetailByDate(DateOnly date)
        {
            return Ok(await _stockService.GetDetailByDate(date));
        }
        [HttpGet("/remain")]
        public async Task<IActionResult> GetStockRemain()
        {
            return Ok(await _stockService.StockRemain());
        }
        [HttpPost("/update")]
        public async Task<IActionResult> UpdateRemainStock([FromBody]UpdateStockRequest updatedStock)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                foreach (var detail in updatedStock.Details)
                {
                    await _stockService.StockUpdate(updatedStock.Id, detail.Ingredient, detail.remainAmount);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
