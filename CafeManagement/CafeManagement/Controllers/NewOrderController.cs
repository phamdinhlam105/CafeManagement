using CafeManagement.Dtos.Request;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Models.Order;
using CafeManagement.Services;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;


namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewOrderController : ControllerBase
    {
        private readonly INewOrderService _newOrderService;
        private readonly INewOrderMapper _newOrderMapper;
        private readonly IOrderService _orderService;

        public NewOrderController(INewOrderService newOrderService, INewOrderMapper newOrderMapper, IOrderService orderService)
        {
            _newOrderService = newOrderService;
            _newOrderMapper = newOrderMapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder([FromBody] NewOrderRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Order order = _newOrderMapper.MapToEntity(req);
            await _newOrderService.CreateOrder(order);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var orders = (await _orderService.GetAll()).ToList();
            var ordersResponse = orders.Select(c => _newOrderMapper.MapToResponse(c)).ToList();
            return Ok(ordersResponse);
        }
        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(_newOrderMapper.MapToResponse(await _newOrderService.GetById(id)));
        }


        [HttpPut("Finish/{orderId}")]
        public async Task<IActionResult> FinishOrder(Guid orderId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Order order = await _newOrderService.GetById(orderId);
            if (order == null)
                return NotFound(orderId);
            return Ok(await _newOrderService.FinishOrder(order));
        }


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
