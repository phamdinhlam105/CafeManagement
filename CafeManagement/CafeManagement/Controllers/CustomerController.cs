using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerMapper _customerMapper;

        public CustomerController(ICustomerService customerService,ICustomerMapper customerMapper)
        {
            _customerService = customerService;
            _customerMapper = customerMapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() 
        {
            var customers= (await _customerService.GetAll()).ToList();
            return  Ok(customers);
        }
        
        [HttpGet("Id")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var customer = await _customerService.GetById(Id);
            return Ok(customer);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] CustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _customerMapper.MapToEntity(customer);
            await _customerService.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id });
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<IActionResult> Edit(Guid Id, [FromBody]  CustomerRequest customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existItem = await _customerService.GetById(Id);
            if (existItem == null)
                return NotFound(new ErrorResponse { Error=404,Message="id customer not found"});
            _customerMapper.UpdateEntityFromRequest(existItem, customer);
            await _customerService.Update(existItem);
            return Ok(existItem);
        }

        [HttpDelete("Id")]
        [Authorize(Roles =Role.Manager)]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var customer = await _customerService.GetById(Id);
            if(customer == null)
                return NotFound();
            await _customerService.Delete(customer);
            return NoContent();
        }

    }
}
