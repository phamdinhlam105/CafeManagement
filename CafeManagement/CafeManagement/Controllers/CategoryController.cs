using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryMapper _categoryMapper;
        private readonly IProductMapper _productMapper;

        public CategoryController(ICategoryService categoryService, ICategoryMapper categoryMapper, IProductMapper productMapper)
        {
            _categoryService = categoryService;
            _categoryMapper = categoryMapper;
            _productMapper = productMapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var categories = (await _categoryService.GetAll()).ToList();
            var categoryResponses = categories.Select(c => _categoryMapper.MapToResponse(c)).ToList();
            if (categoryResponses.Any())
            {
                return Ok(categoryResponses);
            }
            return NotFound();
        }

        [HttpGet("Id")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category == null)
                return NotFound();
            return Ok(_categoryMapper.MapToResponse(category));
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CategoryRequest category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _categoryMapper.MapToEntity(category);
            await _categoryService.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id });
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> Edit(Guid Id, [FromBody] CategoryRequest customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existItem = await _categoryService.GetById(Id);
            if (existItem == null)
                return NotFound();
            _categoryMapper.UpdateEntityFromRequest(existItem, customer);
            await _categoryService.Update(existItem);
            return Ok(existItem);
        }

        [HttpGet("GetProducts/{categoryId}")]
        public async Task<ActionResult> GetProductsByCategory(Guid categoryId)
        {
            var products = await _categoryService.GetProductsByCategory(categoryId);
            var productResponse = products.Select(p => _productMapper.MapToResponse(p)).ToList();
            if (productResponse.Any())
            {
                return Ok(productResponse);
            }
            return NotFound();
        }


        [HttpDelete("Id")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category == null)
                return NotFound();
            await _categoryService.Delete(category);
            return Ok();
        }

    }
}
