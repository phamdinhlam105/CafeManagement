using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models.Order;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CafeManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        // GET: api/<OrderDetailController>
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderDetailMapper _orderDetailMapper;
        private readonly INewOrderService _newOderService;
        private readonly IProductService _productService;

        public OrderDetailController(
            IOrderDetailService orderDetailService,
            INewOrderService newOderService,
            IOrderDetailMapper orderDetailMapper,
            IProductService productService)
        {
            _orderDetailService = orderDetailService;
            _newOderService = newOderService;
            _orderDetailMapper = orderDetailMapper;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDetail([FromBody] OrderDetailRequest req)
        {
            Order currentOrder = await _newOderService.GetById(req.OrderId);
            if (currentOrder == null)
                return NotFound(new ErrorResponse { Error = 404, Message = "id order not found" });
            var product = await _productService.GetById(req.ProductId);
            if (product == null)
                return NotFound(new ErrorResponse { Error = 404, Message = "id product not found" });
            OrderDetail newDetail = _orderDetailMapper.MapToEntity(req);
            await _newOderService.AddOrderDetail(currentOrder,newDetail,product);
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

        [HttpGet("bydate")]
        public async Task<IActionResult> GetDetailsByDate(DateOnly date)
        {
            if (date > Ultilities.GetToday())
                return BadRequest("invalid date");
            return Ok(await _orderDetailService.GetByDate(date));
        }

        [HttpPut("{id}")]
        [Authorize(Roles =Role.Manager)]
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
