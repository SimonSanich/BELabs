using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>
    {
        new Category { Id = 1, Name = "Category1"},
        new Category { Id = 2, Name = "Category2"},
    };

        [HttpGet("/category")]
        public ActionResult<Category> GetCategory(string categoryName)
        {
            var category = categories.FirstOrDefault(p => p.Name == categoryName);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("/category")]
        public IActionResult AddCategory(string categoryName)
        {
            var currentCategory = categories.FirstOrDefault(p => p.Name == categoryName);
            if (currentCategory == null)
            {
                Category category = new Category()
                {
                    Name = categoryName,
                };
                category.Id = categories.Max(p => p.Id) + 1;
                categories.Add(category);
                return Ok(category);
            }

            return BadRequest("You already have this category");
        }

        [HttpDelete("/category")]
        public IActionResult DeleteCategory(string categoryName)
        {
            var category = categories.FirstOrDefault(p => p.Name == categoryName);
            if (category == null)
            {
                return NotFound();
            }
            categories.Remove(category);
            return Ok("You delete this category");
        }
    }
}
