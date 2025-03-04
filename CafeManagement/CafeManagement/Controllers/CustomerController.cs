using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.Services;
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
        public async Task<IActionResult> GetAll() 
        {
            var customers= (await _customerService.GetAll()).ToList();
            var customerResponses = customers.Select(c=>_customerMapper.MapToResponse(c)).ToList();

            return  Ok(customerResponses);
        }
        
        [HttpGet("Id")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var customer = await _customerService.GetById(Id);
            return Ok(_customerMapper.MapToResponse(customer));
        }

        [HttpPost]
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
        public async Task<IActionResult> Edit(Guid Id, [FromBody]  CustomerRequest customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existItem = await _customerService.GetById(Id);
            if (existItem == null)
                return NotFound();
            _customerMapper.UpdateEntityFromRequest(existItem, customer);
            await _customerService.Update(existItem);
            return Ok(existItem);
        }

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
