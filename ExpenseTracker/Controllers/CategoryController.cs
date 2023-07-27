using ExpenseTrackerLogicLayer.Contracts;
using ExpenseTrackerLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.WEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid UserId)
        {
            try
            {
                var response = await _categoryService.Get(UserId);
                if (response.Item1 == null) return BadRequest(response.ErrorMsg);
                return Ok(response.Item1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(BLCategory category)
        {
            try
            {
                var response = await _categoryService.Add(category);
                if (response.category == null) return BadRequest(response.ErrorMsg);
                return Ok(response.category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var response = await _categoryService.Delete(id);
                if (response.category == null) return BadRequest(response.ErrorMsg);
                return Ok(response.category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
