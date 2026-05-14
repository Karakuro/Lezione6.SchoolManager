using Lezione6.SchoolManager.Data;
using Lezione6.SchoolManager.DTO;
using Lezione6.SchoolManager.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Lezione6.SchoolManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(SchoolDbContext ctx, ILogger<StudentController> logger, Mapper mapper, IStudentRepository repo) : ControllerBase
    {
        private readonly SchoolDbContext _ctx = ctx;
        private readonly ILogger<StudentController> _logger = logger;
        private readonly Mapper _mapper = mapper;
        private readonly IStudentRepository _repo = repo;

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
                //return Ok(result);
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
            List<Student> result = _repo.GetAllWithDetails().ToList();
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
                .Include(t => t.Assignments)
                .ThenInclude(a => a.Teacher)
                .Include(t => t.Subjects)
                .SingleOrDefault(t => t.TeacherId == id);
            var moduleSubjects = teacher?.Assignments?.Select(a => a.Module.SubjectId);
            var teacherSubjects = teacher?.Subjects?.Select(s => s.SubjectId);
            var problems = moduleSubjects.Except(teacherSubjects);
            if(problems.Any())
                return;
        }

        [HttpGet]
        [Route("LinqToQuery")]
        public IActionResult GetQuery(List<int> ids)
        {
            var result = from s in _ctx.Students
                         select s;

            result = from s in _ctx.Students
                     where s.Surname == "Sironi"
                     select s;

            var queried = (from s in _ctx.Students
                     join id in ids on s.StudentId equals id
                     select new { s, id }).ToList();

            queried = _ctx.Students.Join(ids, s => s.StudentId, id => id, (s, id) => new { s, id }).ToList();

            //var teachersWithHours = _ctx.Teachers.Include(t => t.Modules)
            //                        .Select(t => new { Teacher = t, TotalHours = t.Modules.Sum(m => m.Hours) });

            var StudentsByMonth = from e in _ctx.Enrollments
                                  group e by e.Date.Month into grouped
                                  select new { Month = grouped.Key, Qty = grouped.Count() };

            return Ok(queried);
        }

        [HttpGet]
        [Route("StudentsFilter")]
        public async Task<IActionResult> GetStudentsByFilter(DateTime? from = null, DateTime? to = null, int? courseId = null)
        {
            if ((from == null && to == null && courseId == null)
                || (from == null && to != null)
                || (from != null && to == null))
                return BadRequest();

            //var count = _ctx.Enrollments.Where(e =>
            //            ((from == null && to == null) || (e.Date <= to && e.Date >= from))
            //            && (courseId == null || e.CourseId == courseId)).Count();

            var enrollments = _ctx.Enrollments.AsQueryable();

            if (from != null && to != null)
                enrollments = enrollments.Where(e => e.Date <= to && e.Date >= from);

            if (courseId != null)
                enrollments = enrollments.Where(e => e.CourseId == courseId);

            return Ok(enrollments.Count());
        }

        [HttpGet]
        [Route("GetTopStudents")]
        public async Task<IActionResult> GetTopStudents()
        {
            var result = _ctx.Courses.Select(c => new
            {
                Course = c,
                Students = c.Enrollments.Select(e => new
                {
                    e.Student,
                    AvgEval = e.Student.Evaluations.Average(ev => ev.Value)
                }).OrderByDescending(s => s.AvgEval).Take(5)
            });

            var result2 = (from c in _ctx.Courses
                           select new
                           {
                               c.CourseId,
                               TopStudents = (from e in c.Enrollments
                                              select new
                                              {
                                                  e.Student,
                                                  AvgEval = (from ev in e.Student.Evaluations
                                                             group ev.Value by ev.StudentId into g
                                                             select g.Average()).Take(5)
                                              })
                           });

            //Incompleto, giusto per far notare quanto sarebbe complesso farlo in questo modo
            //var result2 = (from c in _ctx.Courses
            //               join e in _ctx.Enrollments on c.CourseId equals e.CourseId
            //               join s in _ctx.Students on e.StudentId equals s.StudentId
            //               join ev in _ctx.Evaluations on s.StudentId equals ev.StudentId
            //               group ev by new c into g
            //               select new { Course = g.Key, Students = g.Select(s => new { Student = s}) });

            return Ok(result);
        }

        [HttpGet]
        [Route("GetCountStudentsByTeacher")]
        public async Task<IActionResult> GetCountStudentsByTeacher(int id)
        {
            var count = _ctx.Courses.Where(c => c.Modules.Any(m => m.Assignments.Any(t => t.TeacherId == id))).Sum(c => c.Enrollments.Count);

            return Ok(count);
        }

        [HttpGet]
        [Route("GetTeacherCandidates")]
        public async Task<IActionResult> GetTeacherCandidates()
        {
            var result = _ctx.Students.Include(s => s.Enrollments).Where(s => s.Enrollments.Any(e => e.Date <= DateTime.Now.AddYears(-5))).ToList();

            result = _ctx.Enrollments.Where(e => e.Date <= DateTime.Now.AddYears(-5)).Select(e => e.Student).Distinct().ToList();
            return Ok(result);
        }
    }
}
