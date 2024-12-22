using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController(AppDbContext context) : ControllerBase
    {

        [HttpGet("/category")]
        public async Task<ActionResult> GetCategory(string categoryName)
        {
            var category = await context.Categories
                .Where(c => c.Name == categoryName)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("/category")]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            var currentCategory = await context.Categories
                .AnyAsync(c => c.Name == categoryName);

            if (currentCategory)
            {
                return BadRequest("You already have this category");
            }
 
            Category category = new Category()
            {
                Name = categoryName,
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete("/category")]
        public async Task<IActionResult> DeleteCategory(string categoryName)
        {
            var currentCategory = await context.Categories
              .Where(c => c.Name == categoryName)
              .FirstOrDefaultAsync();

            if (currentCategory == null)
            {
                return BadRequest("There is no category with such category");
            }

            context.Categories.Remove(currentCategory);
            await context.SaveChangesAsync();
            return Ok("You delete this category");
        }
    }
}
