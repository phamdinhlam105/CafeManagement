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
    [Authorize(Policy = "NotCustomer")]
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
            return Ok((await _categoryService.GetAll()).Select(c=>_categoryMapper.MapToResponse(c)));
        }

        [HttpGet("getbyid/{Id}")]
        public async Task<ActionResult> GetById(Guid Id)
        {
            var category = await _categoryService.GetById(Id);
            if (category == null)
                return NotFound();
            return Ok(_categoryMapper.MapToResponse(category));
        }

        [HttpPost]
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        public async Task<ActionResult> Add([FromBody] CategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var item = _categoryMapper.MapToEntity(request);
                var category = await _categoryService.Add(item);
                return Ok(_categoryMapper.MapToResponse(category));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
        public async Task<ActionResult> Edit(Guid Id, [FromBody] CategoryRequest category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var existItem = await _categoryService.GetById(Id);
                if (existItem == null)
                    return NotFound();
                _categoryMapper.UpdateEntityFromRequest(category, existItem);
                var edittedCategory = await _categoryService.Update(existItem);
                return Ok(_categoryMapper.MapToResponse(edittedCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProducts/{categoryId}")]
        public async Task<ActionResult> GetProductsByCategory(Guid categoryId)
        {
            var category = await _categoryService.GetById(categoryId);
            if(category == null) 
                return NotFound(new {Error= 404, Message= "id category not found"});
            var products = await _categoryService.GetProductsByCategory(categoryId);
            return Ok(products.Select(p => _productMapper.MapToResponse(p))) ;
        }


        [HttpDelete("Id")]
        [Authorize(Roles = $"{Role.Manager},{Role.Admin}")]
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
