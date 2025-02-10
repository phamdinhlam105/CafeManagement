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
        public ActionResult<IEnumerable<CustomerResponse>> GetAll() 
        {
            var customers= _customerService.GetAll().ToList();
            var customerResponses = customers.Select(c=>_customerMapper.MapToResponse(c)).ToList();
            if(customerResponses.Any())
            {
                return Ok(customerResponses);
            }
            return NotFound();
        }
        
        [HttpGet("Id")]
        public ActionResult<CustomerResponse> GetById(Guid Id)
        {
            var customer = _customerService.GetById(Id);
            if (customer == null)
                return NotFound();
            return Ok(_customerMapper.MapToResponse(customer));
        }

        [HttpPost]
        public ActionResult Add([FromBody] CustomerRequest customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _customerMapper.MapToEntity(customer);
            _customerService.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id });
        }

        [HttpPut("{Id}")]
        public ActionResult Edit(Guid Id, [FromBody]  CustomerRequest customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existItem = _customerService.GetById(Id);
            if (existItem == null)
                return NotFound();
            _customerMapper.UpdateEntityFromRequest(existItem, customer);
            _customerService.Update(existItem);
            return Ok(existItem);
        }

        [HttpDelete("Id")]
        public ActionResult Delete(Guid Id)
        {
            var customer = _customerService.GetById(Id);
            if(customer == null)
                return NotFound();
            _customerService.Delete(customer);
            return NoContent();
        }

    }
}
