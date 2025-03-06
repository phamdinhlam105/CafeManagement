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
        public async Task<IActionResult> createPromotion(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                await _promotionService.CreatePromotion(promotion);
                return Ok();
            }
               else
                return BadRequest();
        }
        [HttpPost("schedule")]
        [Authorize(Roles = Role.Manager)]
        public async Task<IActionResult> CreatePromotionSchedule(PromotionSchedule promotionSchedule)
        {
            if (ModelState.IsValid)
            {
                await _promotionService.CreatePromotionSchedule(promotionSchedule);
                return Ok();
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
        [HttpPost("apply")]
        [Authorize(Roles = Role.Manager)]
        public async Task<IActionResult> ApplyPromotion(Guid idPromotion,Guid idOrder)
        {
            if (ModelState.IsValid)
            {
                await _promotionService.ApplyPromotionToOrder(idOrder, idPromotion);
                return Ok();
            }
            else
                return BadRequest();
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
