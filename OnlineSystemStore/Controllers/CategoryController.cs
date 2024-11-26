using Microsoft.AspNetCore.Mvc;
using OnlineSystemStore.Domain.DTOs;
using OnlineSystemStore.Services.ServiceInterface;

namespace OnlineSystemStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("Max")]
        public IActionResult GetCategorysMaxNumber()
        {
            int max = _categoryService.Max() + 1;
            return Ok(max);
        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllCategorys()
        {
            var CategorysData = await _categoryService.GetAllCategoryAsync();
            return Ok(CategorysData);
        }

        public async Task<IActionResult> GetCategorysDataTalbe(string Search, string CoulmnName, bool isAsending, int skip, int take)
        {
            var CategorysData = await _categoryService.GetAllCategoryAsync();

            IQueryable<CategoryDto> data = CategorysData.Data.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                data = data.Where(x =>
                x.CategoryName.Equals(Search, StringComparison.CurrentCultureIgnoreCase) ||
                x.CategoryId.ToString().Equals(Search, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(CoulmnName))
            {
                switch (CoulmnName)
                {
                    case "CategoryId": data = isAsending ? data.OrderBy(x => x.CategoryId) : data.OrderByDescending(x => x.CategoryId); break;
                    case "CategoryName": data = isAsending ? data.OrderBy(x => x.CategoryName) : data.OrderByDescending(x => x.CategoryName); break;
                    default: data = data.OrderBy(x => x.CategoryId); break;

                }
            }

            var Resulte = data.Take(take).Skip(skip).ToList();

            return Ok(Resulte);

        }


        [HttpGet("GetByIdAsync/{id}")]
        public async Task<IActionResult> GetCategorysByIdAsync(int id)
        {
            var CategorysData = await _categoryService.GetCategoryByIdAsync(id);

            if (CategorysData == null)
            {
                return NotFound();
            }

            return Ok(CategorysData);
        }
        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddCategorysAsync([FromBody] CategoryDto Category)
        {
            await _categoryService.AddCategoryAsync(Category);
            return Ok();
        }


        [HttpPut("Update/{id}")]
        public IActionResult UpdateCategory([FromBody] CategoryDto Category, int id)
        {
            _categoryService.UpdateCategoryAsync(Category, id);

            return Ok();
        }


        [HttpDelete("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteCategorysAsync(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return Ok();
        }
    }
}
