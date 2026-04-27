using Lezione6.SchoolManager.Data;
using Lezione6.SchoolManager.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(SchoolDbContext ctx, ILogger<StudentController> logger, Mapper mapper) : ControllerBase
    {
        private readonly SchoolDbContext _ctx = ctx;
        private readonly ILogger<StudentController> _logger = logger;
        private readonly Mapper _mapper = mapper;

        //public StudentController(SchoolDbContext ctx) 
        //{
        //    _ctx = ctx;
        //}
        // GET /api/Student
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _ctx.Students.Include(s => s.Enrollments);
                return Ok(result);
                var students = result.Select(s => new StudentDto()
                {
                    Id = s.StudentId,
                    Name = s.Name,
                    Surname = s.Surname
                });
                return Ok(students);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var student = _ctx.Students.SingleOrDefault(s => s.StudentId == id);
            if (student == null)
                return BadRequest();
            return Ok(_mapper.MapEntityToDto(student));
        }

        // GET /api/Student/Details
        [HttpGet]
        [Route("Details")]
        public IActionResult GetAllWithDetails()
        {
            List<Student> result = _ctx.Students
                                    .Include(s => s.Enrollments)
                                    .ThenInclude(e => e.Course).ToList();
            List<StudentDto> students = result.ConvertAll(_mapper.MapEntityToDto);
            return Ok(students);
        }

        [HttpPost]
        public IActionResult Create([FromBody]StudentDto dto)
        {
            Student student = new Student()
            {
                StudentId = 0,
                Name = dto.Name,
                Surname = dto.Surname
            };
            _ctx.Students.Add(student);
            if (_ctx.SaveChanges() == 1)
                return NoContent();
            else
                return UnprocessableEntity();
                //StatusCode(StatusCodes.Status422UnprocessableEntity);
        }

        // PUT /api/student/1
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] StudentDto dto)
        {
            var student = _ctx.Students.SingleOrDefault(s => s.StudentId == id);
            if (student == null)
                return BadRequest();

            student.Name = dto.Name;
            student.Surname = dto.Surname;
            if (_ctx.SaveChanges() == 1)
                return NoContent();
            else
                return UnprocessableEntity();
        }

        // DELETE api/student/1
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _ctx.Students.SingleOrDefault(s => s.StudentId == id);
            if (student == null)
                return BadRequest();
            _ctx.Students.Remove(student);
            if (_ctx.SaveChanges() == 1)
                return NoContent();
            else
                return UnprocessableEntity();
        }

        public void Check(int id)
        {
            var teacher = _ctx.Teachers
                .Include(t => t.Modules)
                .Include(t => t.Subjects)
                .SingleOrDefault(t => t.TeacherId == id);
            var moduleSubjects = teacher?.Modules?.Select(m => m.SubjectId);
            var teacherSubjects = teacher?.Subjects?.Select(s => s.SubjectId);
            var problems = moduleSubjects.Except(teacherSubjects);
            if(problems.Any())
                return;
        }
    }
}
