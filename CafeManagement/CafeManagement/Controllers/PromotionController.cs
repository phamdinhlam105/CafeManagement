using CafeManagement.Dtos.Request;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Models.PromotionModel;
using CafeManagement.Services.PromotionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPost("promotion")]
        public async Task<IActionResult> CreatePromotion([FromBody] Promotion promotion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _promotionService.CreatePromotion(promotion));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
               
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPost("schedule")]
        public async Task<IActionResult> CreatePromotionSchedule([FromBody]PromotionSchedule promotionSchedule)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                return Ok(await _promotionService.CreatePromotionSchedule(promotionSchedule));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("promotion")]
        public async Task<IActionResult> GetAllPromotion()
        {
            return Ok(await _promotionService.GetAllPromotions());
        }
        [HttpGet("schedule")]
        public async Task<IActionResult> GetActivePromotionByDate(DateOnly startDate,DateOnly endDate)
        {
            return Ok(await _promotionService.GetActivePromotionByDate(startDate, endDate));
        }
        [HttpGet("schedule/byPromotionId/{id}")]
        public async Task<IActionResult> GetScheduleByPromotionId(Guid id)
        {
            return Ok(await _promotionService.GetScheduleByPromotionId(id));
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPut("promotion/{promotionId}")]
        public async Task<IActionResult> updatePromotion(Guid promotionId, [FromBody] EditPromotionRequest request)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            try
            {
                var promotion = new Promotion
                {
                    Name = request.Name,
                    Description = request.Description,
                    Discount = request.Discount
                };
                await _promotionService.UpdatePromotion(promotionId, promotion);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPut("schedule/{scheduleId}")]
        public async Task<IActionResult> updatePromotionSchedule(Guid scheduleId, [FromBody] PromotionSchedule schedule)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            try
            {
                await _promotionService.UpdatePromotionSchedule(scheduleId, schedule);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
