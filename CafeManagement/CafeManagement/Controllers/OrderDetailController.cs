using CafeManagement.Dtos.Request;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
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
        public IActionResult AddDetail([FromBody] OrderDetailRequest req)
        {
            Order currentOrder = _newOderService.GetById(req.OrderId);
            if (currentOrder == null)
                return NotFound(req.OrderId);
            OrderDetail newDetail = _orderDetailMapper.MapToEntity(req);
            _orderDetailService.Add(newDetail);
            _newOderService.AddOrderDetail(currentOrder,newDetail);
            return Ok(newDetail);
        }

        [HttpGet("GetByOrderId/{orderId}")]
        public IActionResult GetDetailByOrder(Guid orderId) 
        {
            ICollection<OrderDetail> listDetail = _orderDetailService.GetDetailsByOrder(orderId);
            if (listDetail == null || listDetail.Count == 0)
                return NoContent();
            return Ok(listDetail.Select(d => _orderDetailMapper.MapToResponse(d)));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderDetail> details = _orderDetailService.GetAll();
            return Ok(details);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] OrderDetailRequest request)
        {
            OrderDetail orderDetail = _orderDetailService.GetById(id);
            if (orderDetail == null)
                return NotFound(id);
            _orderDetailMapper.UpdateEntityFromRequest(orderDetail, request);
            _orderDetailService.Update(orderDetail);
            return Ok();
        }
    }
}
