using Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordController(AppDbContext context): ControllerBase
    {

        [HttpGet("/record/{record_id}")]
        public async Task<ActionResult> GetRecordById(Guid record_id)
        {
            var record =  await context.Records
                .Where(r => r.Id == record_id)
                .Select(r => new RecordResponse(
                    r.Id,
                    r.User.Name,
                    r.Category.Name,
                    r.CreatedAt,
                    r.TotalAmount,
                    r.Currency.Name))
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        [HttpDelete("/record/{record_id}")]
        public async Task<IActionResult> DeleteRecordById(Guid record_id)
        {
            var record = await context.Records
                .Where(r => r.Id == record_id)
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound();
            }
            context.Records.Remove(record);

            return Ok("You delete this record");
        }

        [HttpPost("/record")]
        public async Task<IActionResult> AddRecord(Guid userId, int categoryId, decimal total, int? currencyId = null)
        {
            var user = await context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
                
            if (user == null)
            {
                return BadRequest("user doesn't exist");
            }

            var category = await context.Categories
                .Where(u => u.Id == categoryId)
                .FirstOrDefaultAsync();

            if (category == null)
            {
                return BadRequest("category doesn't exists");
            }

            Currency? currency;
            if (currencyId.HasValue)
            {
                currency = await context.Currency
                    .FindAsync(currencyId.Value);

                if (currency == null)
                {
                    return BadRequest("Invalid currency ID.");
                }
            }
            else
            {
                currency = user.Currency;
            }

            var record = new Record
            {
                UserId = userId,
                CategoryId = categoryId,
                CreatedAt = DateTime.UtcNow,
                TotalAmount = total,
                CurrencyId = currency.Id
            };

            await context.Records.AddAsync(record);
            await context.SaveChangesAsync();
            return Ok(record);
        }

        [HttpGet("/record")]
        public async Task<ActionResult> GetRecord(Guid? userId, int? categoryId)
        {
            if (userId == null && categoryId == null)
            {
                return BadRequest("You don't write a params");
            }

            var filteredRecords = context.Records.AsQueryable();

            if (userId.HasValue)
            {
                filteredRecords = filteredRecords
                    .Where(p => p.UserId == userId);
            }
            if (categoryId.HasValue)
            {
                filteredRecords = filteredRecords
                    .Where(p => p.CategoryId == categoryId);
            }

            var result = await filteredRecords
                .Select(r => new RecordResponse(
                    r.Id,
                    r.User.Name,
                    r.Category.Name,
                    r.CreatedAt,
                    r.TotalAmount,
                    r.Currency.Name))
                .ToListAsync();

            if (!result.Any())
            {
                return NotFound("No records found with the specified criteria.");
            }
            return Ok(result);
        }
    }

    public record RecordResponse(Guid Id, string UserName, string CategoryName, DateTime CreatedAt, decimal TotalAmount, string CurrencyName);
}
