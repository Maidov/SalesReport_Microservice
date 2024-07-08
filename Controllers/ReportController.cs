using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReportApi.Models;

namespace ReportApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportController : ControllerBase
    {
        private readonly SalesContext _context;

        public SalesReportController(SalesContext context)
        {
            _context = context;
        }

        [HttpGet("monthly-report")]
        public async Task<ActionResult<MonthlyReport>> GetMonthlyReport(int year, int month, string category)
        {
            try
            {
                // Проверка на корректность месяца (1-12)
                if (month < 1 || month > 12)
                {
                    return BadRequest("Invalid month. Please provide a valid month number (1-12).");
                }
                if (year < 1)
                {
                    return BadRequest("Invalid year. Please provide a valid year greater than zero.");
                }

                var salesItems = await _context.SalesItems
                    .Where(si => si.Category == category &&
                                 si.Date.Year == year &&
                                 si.Date.Month == month)
                    .ToListAsync();

                // Группируем по наименованию товара и вычисляем средние показатели
                var groupedItems = salesItems.GroupBy(si => si.Name)
                    .Select(group => new ReportItem
                    {
                        Name = group.Key,
                        AverageRevenue = (decimal)group.Average(si => si.Revenue),
                        AverageExpenses = (decimal)group.Average(si => si.Expenses),
                        AverageIncome = (decimal)group.Average(si => si.Income)
                    })
                    .ToList();

                // Формируем объект с результатами отчета
                var report = new MonthlyReport
                {
                    Category = category,
                    Month = month,
                    Year = year,
                    Items = groupedItems
                };

                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}