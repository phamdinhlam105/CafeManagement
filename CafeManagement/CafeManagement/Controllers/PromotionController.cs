using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Models.PromotionModel;
using CafeManagement.Services.PromotionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private IPromotionService _promotionService;
        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        [HttpPost("promotion")]
        public IActionResult createPromotion(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _promotionService.CreatePromotion(promotion);
                return Ok();
            }
               else
                return BadRequest();
        }
        [HttpPost("schedule")]
        public IActionResult CreatePromotionSchedule(PromotionSchedule promotionSchedule)
        {
            if (ModelState.IsValid)
            {
                _promotionService.CreatePromotionSchedule(promotionSchedule);
                return Ok();
            }
            else
                return BadRequest();
        }
        [HttpGet("promotion")]
        public IActionResult GetAllPromotion()
        {
            return Ok(_promotionService.GetAllPromotions());
        }
        [HttpGet("schedule")]
        public IActionResult GetActivePromotionByDate(DateOnly startDate,DateOnly endDate)
        {
            return Ok(_promotionService.GetActivePromotionByDate(startDate, endDate));
        }
        [HttpPost("apply")]
        public IActionResult ApplyPromotion(Guid idPromotion,Guid idOrder)
        {
            if (ModelState.IsValid)
            {
                _promotionService.ApplyPromotionToOrder(idOrder, idPromotion);
                return Ok();
            }
            else
                return BadRequest();
        }
        [HttpPut("promotion")]
        public IActionResult updatePromotion(Guid promotionId, Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _promotionService.UpdatePromotion(promotionId, promotion);
                return Ok();
            }
            else
                return BadRequest();
        }
        [HttpPut("schedule")]
        public IActionResult updatePromotionSchedule(Guid scheduleId, PromotionSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                _promotionService.UpdatePromotionSchedule(scheduleId, schedule);
                return Ok();
            }
            else
                return BadRequest();
        }
    }
}
