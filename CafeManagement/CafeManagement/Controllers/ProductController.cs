using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services.ProductService;
using CafeManagement.Mappers;
using CafeManagement.Models;
using CafeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
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
            return Ok((await _productService.GetAll()).Select(p=>_productMapper.MapToResponse(p)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Ok(_productMapper.MapToResponse(await _productService.GetById(id)));
        }

        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var item = _productMapper.MapToEntity(product);
                var newProduct = await _productService.Add(item);
                return Ok(_productMapper.MapToResponse(newProduct));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(Guid id,[FromBody] ProductRequest product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var existingProduct = await _productService.GetById(id);
                if (existingProduct == null)
                    return NotFound();
                _productMapper.UpdateEntityFromRequest(product, existingProduct);
                var edittedProduct = await _productService.Update(existingProduct);
                return Ok(_productMapper.MapToResponse(edittedProduct));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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

