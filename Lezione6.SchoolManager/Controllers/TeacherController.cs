using Lezione6.SchoolManager.Data;
using Lezione6.SchoolManager.DTO;
using Lezione6.SchoolManager.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController(SchoolDbContext ctx, ILogger<TeacherController> logger, Mapper mapper) : ControllerBase
    {
        private readonly SchoolDbContext _ctx = ctx;
        private readonly ILogger<TeacherController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        [HttpGet]
        public async Task<IActionResult> GetAllBySeniority(int? minSeniority = null)
        {
            var result = await _ctx.Teachers
                .Where(t => DateTime.Now.Year - t.YearFirstEmployed >= (minSeniority ?? 0))
                .OrderBy(t => t.YearFirstEmployed)
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetTotalHoursByCourseAndTeacher")]
        public async Task<IActionResult> GetTotalHoursByCourseAndTeacher()
        {
            var result = _ctx.Courses.Select(c => new
            {
                Course = c,
                Assignments = c.Modules.SelectMany(m => m.Assignments)
                                        .GroupBy(a => a.Teacher)
                                        .Select(g => new { 
                                            Teacher = g.Key, 
                                            Hours = g.Sum(a => a.AssignedHours) 
                                        })
            });

            return Ok(result);
        }
    }
}
