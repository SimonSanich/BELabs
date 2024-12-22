using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(AppDbContext context) : ControllerBase
    {

        [HttpGet("/user/{id}")]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            var user = await context.Users
                .Where(u => u.Id == id)
                .FirstOrDefaultAsync();
                
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpDelete("/user/{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var user = context.Users
                .FirstOrDefault(p => p.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            context.Remove(user);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("/user")]
        public async Task<IActionResult> AddUser(string userName, int? currencyId)
        {
            var isUser = await context.Users
                .AnyAsync(u => u.Name == userName);
            if (isUser)
            {
                return BadRequest("User with such name exists");
            }

            var isCurrency = await context.Currency
                .AnyAsync(u => u.Id ==  currencyId);

            if (!isCurrency)
            {
                return BadRequest("Currence doesnt exist");
            }

            var user = new User()
            {
                Name = userName,
                CurrencyId = currencyId
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpGet("/users")]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await context.Users
                .ToListAsync();

            return Ok(users);
                
        }
    }
}
