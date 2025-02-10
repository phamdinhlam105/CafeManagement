using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Models;
using CafeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductMapper _productMapper;
        public ProductController(IProductService productService, IProductMapper productMapper)
        {
            _productService = productService;
            _productMapper = productMapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductResponse>> GetAll()
        {
            var products = _productService.GetAll().ToList();
            var productResponse = products.Select(c => _productMapper.MapToResponse(c)).ToList();
            if (productResponse.Any())
            {
                return Ok(productResponse);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductResponse> GetById(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();
            return Ok(_productMapper.MapToResponse(product));
        }

     
        [HttpPost]
        public ActionResult Add([FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var item = _productMapper.MapToEntity(product);
            _productService.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id });
        }

        [HttpPut("{id}")]
        public ActionResult Edit(Guid id,[FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = _productService.GetById(id);
            if (existingProduct == null)
                return NotFound();
            _productMapper.UpdateEntityFromRequest(existingProduct, product);
            _productService.Update(existingProduct);
            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            _productService.Delete(product);
            return NoContent();
        }
    }
}

