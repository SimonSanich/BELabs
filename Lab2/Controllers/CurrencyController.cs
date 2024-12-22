using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [ApiController]
    public class CurrencyController(AppDbContext context) : ControllerBase
    {
        [HttpGet("/currencies")]
        public async Task<ActionResult> GetAllCurrenciesAsync()
        {
            var currencies = await context.Currency.ToListAsync();
            return Ok(currencies);
        }

        [HttpPost("/currency")]
        public async Task<IActionResult> AddCurrencyAsync(string name)
        {
           
            if (await context.Currency.AnyAsync(c => c.Name == name))
            {
                return BadRequest("Currency with this name already exists.");
            }

            var currency = new Currency { Name = name };
            context.Currency.Add(currency);
            await context.SaveChangesAsync();

            return Ok("Currency added successfully.");
        }

        [HttpDelete("/currency")]
        public async Task<IActionResult> DeleteCurrencyAsync(string name)
        {
            var currency = await context.Currency.FirstOrDefaultAsync(c => c.Name == name);
            if (currency == null)
            {
                return NotFound("Currency not found.");
            }

            context.Currency.Remove(currency);
            await context.SaveChangesAsync();
            return Ok("Currency deleted successfully.");
        }
    }
}
