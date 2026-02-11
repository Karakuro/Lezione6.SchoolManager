using Lezione6.SchoolManager.Data;
using Lezione6.SchoolManager.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(SchoolDbContext ctx) : ControllerBase
    {
        private readonly SchoolDbContext _ctx = ctx;

        //public StudentController(SchoolDbContext ctx) 
        //{
        //    _ctx = ctx;
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Student> result = _ctx.Students
                                    .Include(s => s.Enrollments)
                                    .ThenInclude(e => e.Course).ToList();
            List<StudentDto> students = result.ConvertAll(s => new StudentDto()
            {
                Name = s.Name,
                Surname = s.Surname,
                Id = s.StudentId,
                Courses = new Dictionary<int, string>(
                    s.Enrollments.Select(e => new KeyValuePair<int, string>
                    (e.Course.CourseId, e.Course.Title)
                ))
            });
            return Ok(_ctx.Students);
        }
    }
}
