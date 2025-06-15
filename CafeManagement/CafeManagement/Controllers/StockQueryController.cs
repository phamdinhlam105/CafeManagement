
using CafeManagement.Interfaces.Facade.StockFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockQueryController : ControllerBase
    {
        private readonly IStockQueryUseCase _stockQueryUseCase;
        public StockQueryController(IStockQueryUseCase stockQueryUseCase)
        {
            _stockQueryUseCase = stockQueryUseCase;
        }
        [HttpGet("stock")]
        public async Task<IActionResult> GetStockByDate(DateOnly? date)
        {
            try
            {
                return Ok(await _stockQueryUseCase.GetStockDetailByDate(date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-ingredient/{id}")]
        public async Task<IActionResult> GetByIngredient(Guid id)
        {
            try
            {
                return Ok(await _stockQueryUseCase.GetStockByIngredientId(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("entry")]
        public async Task<IActionResult> GetEntryByDate(DateOnly? date)
        {
            try
            {
                return Ok(await _stockQueryUseCase.GetEntriesByDate(date));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("adjustment")]
        public async Task<IActionResult> GetAdjustmentByDate(DateOnly? date)
        {
            try
            {
                return Ok(await _stockQueryUseCase.GetAdjustmentsByDate(date));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
