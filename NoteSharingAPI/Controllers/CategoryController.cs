using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        private readonly NoteDbContext _noteDbContext;

        public CategoryController(ICategoryService categoryService, NoteDbContext noteDbContext)
        {
            _categoryService = categoryService;
            _noteDbContext = noteDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = _categoryService.TGetList();
            return Ok(categories);
        }
    }
}
