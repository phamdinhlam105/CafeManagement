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
        public ActionResult<IEnumerable<CategoryResponse>> GetAll()
        {
            var categories = _categoryService.GetAll().ToList();
            var categoryResponses = categories.Select(c => _categoryMapper.MapToResponse(c)).ToList();
            if (categoryResponses.Any())
            {
                return Ok(categoryResponses);
            }
            return NotFound();
        }

        [HttpGet("Id")]
        public ActionResult<CategoryResponse> GetById(Guid Id)
        {
            var category = _categoryService.GetById(Id);
            if (category == null)
                return NotFound();
            return Ok(_categoryMapper.MapToResponse(category));
        }

        [HttpPost]
        public ActionResult Add([FromBody] CategoryRequest category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _categoryMapper.MapToEntity(category);
            _categoryService.Add(item);
            return CreatedAtAction(nameof(GetById), new { item.Id });
        }

        [HttpPut("{Id}")]
        public ActionResult Edit(Guid Id, [FromBody] CategoryRequest customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existItem = _categoryService.GetById(Id);
            if (existItem == null)
                return NotFound();
            _categoryMapper.UpdateEntityFromRequest(existItem, customer);
            _categoryService.Update(existItem);
            return Ok(existItem);
        }

        [HttpGet("GetProducts/{categoryId}")]
        public ActionResult<IEnumerable<ProductResponse>> GetProductsByCategory(Guid categoryId)
        {
            var products = _categoryService.GetProductsByCategory(categoryId);
            var productResponse = products.Select(p => _productMapper.MapToResponse(p)).ToList();
            if (productResponse.Any())
            {
                return Ok(productResponse);
            }
            return NotFound();
        }


        [HttpDelete("Id")]
        public ActionResult Delete(Guid Id)
        {
            var category = _categoryService.GetById(Id);
            if (category == null)
                return NotFound();
            _categoryService.Delete(category);
            return NoContent();
        }

    }
}
