using CafeManagement.Dtos.Request;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;
using CafeManagement.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        // GET: api/<OrderDetailController>
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderDetailMapper _orderDetailMapper;
        private readonly INewOrderService _newOderService;

        public OrderDetailController(
            IOrderDetailService orderDetailService,
            INewOrderService newOderService,
            IOrderDetailMapper orderDetailMapper)
        {
            _orderDetailService = orderDetailService;
            _newOderService = newOderService;
            _orderDetailMapper = orderDetailMapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddDetail([FromBody] OrderDetailRequest req)
        {
            Order currentOrder = await _newOderService.GetById(req.OrderId);
            if (currentOrder == null)
                return NotFound(req.OrderId);
            OrderDetail newDetail = _orderDetailMapper.MapToEntity(req);
            await _orderDetailService.Add(newDetail);
            await _newOderService.AddOrderDetail(currentOrder, newDetail);
            return Ok(newDetail);
        }

        [HttpGet("GetByOrderId/{orderId}")]
        public async Task<IActionResult> GetDetailByOrder(Guid orderId) 
        {
            var listDetail = (await _orderDetailService.GetDetailsByOrder(orderId)).ToList();
            if (listDetail == null || listDetail.Count == 0)
                return NoContent();
            return Ok(listDetail.Select(d => _orderDetailMapper.MapToResponse(d)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var details = (await _orderDetailService.GetAll()).ToList();
            return Ok(details);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] OrderDetailRequest request)
        {
            OrderDetail orderDetail = await _orderDetailService.GetById(id);
            if (orderDetail == null)
                return NotFound(id);
            _orderDetailMapper.UpdateEntityFromRequest(orderDetail, request);
            await _orderDetailService.Update(orderDetail);
            return Ok();
        }
    }
}
