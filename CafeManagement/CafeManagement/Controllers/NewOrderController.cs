﻿using CafeManagement.Dtos.Request;
using CafeManagement.Enums;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Models.Order;
using CafeManagement.Services;
using CafeManagement.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IActionResult NewOrder([FromBody] NewOrderRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Order order = _newOrderMapper.MapToEntity(req);
            _newOrderService.CreateOrder(order);
            return Ok(order);
        }

        [HttpGet]
        public IActionResult Getall()
        {
            var orders = _orderService.GetAll().ToList();
            var ordersResponse = orders.Select(c => _newOrderMapper.MapToResponse(c)).ToList();
            if (ordersResponse.Any())
            {
                return Ok(ordersResponse);
            }
            return NotFound();
        }
        [HttpGet("/{id}")]
        public IActionResult GetById(Guid id)
        {
            Order order = _newOrderService.GetById(id);
            if (order == null)
                return NotFound();
            return Ok(_newOrderMapper.MapToResponse(order));
        }


        [HttpPut("ChangeStatus/{orderId}")]
        public IActionResult ChangeStatus(Guid orderId, [FromBody]  OrderStatus orderStatus)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            Order order = _newOrderService.GetById(orderId);
            if (order == null)
                return NotFound(orderId);
            _newOrderService.ChangeStatus(order, orderStatus);
            return Ok(_newOrderMapper.MapToResponse(order));
        }


        [HttpDelete]
        public IActionResult DeleteOrder(Guid orderId)
        {
            Order order = _newOrderService.GetById(orderId);
            if (order == null)
                return NotFound(orderId);
            return NoContent();
        }
    }
}
