using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Mappers;
using CafeManagement.Models;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize]
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
        public async Task<ActionResult> GetAll()
        {
            var products = (await _productService.GetAll()).ToList();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(await _productService.GetById(id));
        }

        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var item = _productMapper.MapToEntity(product);
            await _productService.Add(item);
            return Ok(item);
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(Guid id,[FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await _productService.GetById(id);
            if (existingProduct == null)
                return NotFound();
            _productMapper.UpdateEntityFromRequest(existingProduct, product);
            await _productService.Update(existingProduct);
            return Ok(existingProduct);
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            await _productService.Delete(product);
            return Ok();
        }
    }
}

