using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult> GetAll()
        {
            var categories = (await _categoryService.GetAll()).ToList();
            return Ok(categories);
        }

        [HttpGet("Id")]
        [Authorize]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = Role.Manager)]
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
        [Authorize(Roles = Role.Manager)]
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
        [Authorize]
        public async Task<ActionResult> GetProductsByCategory(Guid categoryId)
        {
            var category = await _categoryService.GetById(categoryId);
            if(category == null) 
                return NotFound(new ErrorResponse{Error= 404, Message= "id category not found"});
            var products = await _categoryService.GetProductsByCategory(categoryId);
            return Ok(products);
        }


        [HttpDelete("Id")]
        [Authorize(Roles = Role.Manager)]
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
