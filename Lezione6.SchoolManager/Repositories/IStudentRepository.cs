using Lezione6.SchoolManager.Data;

namespace Lezione6.SchoolManager.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        IEnumerable<Student> GetAllWithDetails();
    }
}
