using Lezione6.SchoolManager.Data;
using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Repositories
{
    public class StudentRepository(SchoolDbContext _ctx) : GenericRepository<Student>(_ctx), IStudentRepository
    {
        //public StudentRepository(SchoolDbContext _ctx) : base(_ctx) { }
        public IEnumerable<Student> GetAllWithDetails()
        {
            return _ctx.Students.Include(s => s.Enrollments).ThenInclude(e => e.Course);
        }
    }
}
