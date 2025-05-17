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
    [Authorize(Policy = "NotCustomer")]
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
        public async Task<IActionResult> GetAll() 
        {
            var customers= (await _customerService.GetAll()).ToList();
            return  Ok(customers);
        }
        
        [HttpGet("getbyid/{Id}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var customer = await _customerService.GetById(Id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var item = _customerMapper.MapToEntity(customer);
                var newCustomer = await _customerService.Add(item);
                return Ok(newCustomer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Edit(Guid Id, [FromBody]  CustomerRequest customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {

                var existItem = await _customerService.GetById(Id);
                if (existItem == null)
                    return NotFound(new  { Error = 404, Message = "id customer not found" });
                _customerMapper.UpdateEntityFromRequest(customer, existItem);
                await _customerService.Update(existItem);
                return Ok(existItem);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpDelete("Id")]
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
