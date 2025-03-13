using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Models.PromotionModel;
using CafeManagement.Services.PromotionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
   
        [HttpPost("promotion")]
        [Authorize(Roles = Role.Manager)]
        public async Task<IActionResult> CreatePromotion(Promotion promotion)
        {
            if (ModelState.IsValid)
                return Ok(await _promotionService.CreatePromotion(promotion));
            else
                return BadRequest();
        }
        [HttpPost("schedule")]
        [Authorize(Roles = Role.Manager)]
        public async Task<IActionResult> CreatePromotionSchedule(PromotionSchedule promotionSchedule)
        {
            if (ModelState.IsValid)
            {
                
                return Ok(await _promotionService.CreatePromotionSchedule(promotionSchedule));
            }
            else
                return BadRequest();
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

        [HttpPut("promotion")]
        [Authorize(Roles = Role.Manager)]
        public async Task<IActionResult> updatePromotion(Guid promotionId, Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                await _promotionService.UpdatePromotion(promotionId, promotion);
                return Ok();
            }
            else
                return BadRequest();
        }
        [HttpPut("schedule")]
        [Authorize(Roles = Role.Manager)]
        public async Task<IActionResult> updatePromotionSchedule(Guid scheduleId, PromotionSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                await _promotionService.UpdatePromotionSchedule(scheduleId, schedule);
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
