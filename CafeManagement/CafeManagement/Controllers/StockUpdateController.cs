
using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Facade.StockFacade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockUpdateController : ControllerBase
    {
        private readonly IStockUpdateUseCase _stockUpdateUseCase;
        public StockUpdateController(IStockUpdateUseCase stockUpdateUseCase)
        {
            _stockUpdateUseCase = stockUpdateUseCase;
        }

        [HttpPost("entry")]
        public async Task<IActionResult> ImportStock([FromBody] StockEntryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _stockUpdateUseCase.StockImport(request);
                return Ok();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("adjustment")]
        public async Task<IActionResult> NewStockAdjustment([FromBody] StockAdjustmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _stockUpdateUseCase.NewAdjustment(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
