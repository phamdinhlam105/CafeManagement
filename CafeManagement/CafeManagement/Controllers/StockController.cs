using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly IStockMapper _stockMapper;
        public StockController(IStockService stockService, IStockMapper stockMapper)
        {
            _stockService = stockService;
            _stockMapper = stockMapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            return Ok(await _stockService.GetAllDailyStocks());
        }
        [HttpGet("today")]
        public async Task<IActionResult> GetToDayStock()
        {
            try
            {
                return Ok(_stockMapper.MapToResponse(await _stockService.NewDailyStock()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("bydate/stock")]
        public async Task<IActionResult> GetDetailByDate(DateOnly date)
        {
            return Ok((await _stockService.GetDetailByDate(date)).Select(dsd=>new StockDetailResponse
            {
                Id=dsd.Id,
                StockAtStartOfDay=dsd.StockAtStartOfDay,
                StockImport=dsd.StockImport,
                StockRemaining = dsd.StockRemaining,
                Ingredient=dsd.Ingredient,
                IngredientId=dsd.IngredientId,
                DailyStockId=dsd.DailyStockId
            }));
        }
        [HttpGet("remain")]
        public async Task<IActionResult> GetStockRemain()
        {
            try
            {
                return Ok(_stockMapper.MapToResponse(await _stockService.StockRemain()));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateRemainStock([FromBody]UpdateStockRequest updatedStock)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                foreach (var detail in updatedStock.Details)
                {
                    await _stockService.StockUpdate(detail.IngredientId, detail.remainAmount);
                }
                var today = await _stockService.NewDailyStock();
                return Ok(_stockMapper.MapToResponse(today));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
