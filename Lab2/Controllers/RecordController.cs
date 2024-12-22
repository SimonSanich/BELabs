﻿using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordController: ControllerBase
    {
        private static List<Record> records = new List<Record>
    {
        new Record { Id = 1, UserId = 1, CategoryId = 1, CreatedAt = DateTime.Now, TotalAmount = 1},
        new Record { Id = 2, UserId = 3, CategoryId = 1, CreatedAt = DateTime.Now, TotalAmount = 2},
        new Record { Id = 3, UserId = 2, CategoryId = 2, CreatedAt = DateTime.Now, TotalAmount = 3},
        new Record { Id = 4, UserId = 2, CategoryId = 3, CreatedAt = DateTime.Now, TotalAmount = 4}
    };

        [HttpGet("/record/{record_id}")]
        public ActionResult<Record> GetRecordById(int record_id)
        {
            var record = records.FirstOrDefault(p => p.Id == record_id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpDelete("/record/{record_id}")]
        public IActionResult DeleteRecordById(int record_id)
        {
            var record = records.FirstOrDefault(p => p.Id == record_id);
            if (record == null)
            {
                return NotFound();
            }
            records.Remove(record);
            return Ok("You delete this record");
        }

        [HttpPost("/record")]
        public IActionResult AddRecord(int userId, int categoryId, decimal total)
        {
            Record record = new Record()
            {
                UserId = userId,
                CategoryId = categoryId,
                CreatedAt = DateTime.Now,
                TotalAmount = total
            };
            record.Id = records.Max(p => p.Id) + 1;
            records.Add(record);
            return CreatedAtAction(nameof(GetRecordById), new { id = record.Id }, record);
        }

        [HttpGet("/record")]
        public ActionResult GetRecord(int? userId, int? categoryId)
        {
            if (userId == null && categoryId == null)
            {
                return BadRequest("You don't write a params");
            }

            var filteredRecords = records.AsQueryable();
            if (userId != null)
            {
                filteredRecords = filteredRecords.Where(p => p.UserId == userId);
            }
            if (categoryId != null)
            {
                filteredRecords = filteredRecords.Where(p => p.CategoryId == categoryId);
            }

            if (!filteredRecords.Any())
            {
                return NotFound("No records found with the specified criteria.");
            }
            return Ok(filteredRecords);
        }
    }
}
