using CafeManagement.Dtos.Request.OrderReq;
using CafeManagement.Enums;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Models.Order;
using CafeManagement.Services;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewOrderController : ControllerBase
    {
        private readonly INewOrderService _newOrderService;
        private readonly INewOrderMapper _newOrderMapper;

        public NewOrderController(INewOrderService newOrderService, INewOrderMapper newOrderMapper)
        {
            _newOrderService = newOrderService;
            _newOrderMapper = newOrderMapper;
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder([FromBody] NewOrderRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Order order = _newOrderMapper.MapToEntity(req);
                var newOrder = await _newOrderService.CreateOrder(order);
                return Ok(_newOrderMapper.MapToResponse(newOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var orders = (await _newOrderService.GetAll()).ToList();
            return Ok(orders.Select(o => _newOrderMapper.MapToResponse(o)));
        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_newOrderMapper.MapToResponse(await _newOrderService.GetById(id)));
        }


        [HttpPost("Finish/{orderId}")]
        public async Task<IActionResult> FinishOrder(Guid orderId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Order order = await _newOrderService.GetById(orderId);
                if (order == null)
                    return NotFound(orderId);
                await _newOrderService.FinishOrder(order);
                return Ok(new { Message= "Success"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            try
            {
                await _newOrderService.CancelOrder(orderId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(Guid orderId)
        {
            Order order = await _newOrderService.GetById(orderId);
            if (order == null)
                return NotFound(orderId);
            return Ok();
        }
    }
}
