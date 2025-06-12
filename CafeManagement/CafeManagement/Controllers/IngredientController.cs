using CafeManagement.Dtos.Respone;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagement.Controllers
{
    [Authorize(Policy = "NotCustomer")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
            public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ingredientService.GetAll());
        }
        [HttpPost]
        public async Task<IActionResult> AddIngredient([FromBody]Ingredient ingredient)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                var newIngredient = await _ingredientService.Add(ingredient);
                return Ok(newIngredient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> AddIngredient(Guid Id, [FromBody] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                if (await _ingredientService.GetById(Id) == null)
                    return NotFound("Id not found");
                var updatedIngredient = await _ingredientService.Update(ingredient);
                if (updatedIngredient == null)
                    return NotFound(new  { Error = 404, Message = "Id not found" });
                return Ok(updatedIngredient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
