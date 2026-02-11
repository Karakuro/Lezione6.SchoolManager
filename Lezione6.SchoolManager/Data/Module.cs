using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Data
{
    public class Module
    {
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public required string Title { get; set; }
        public Course? Course { get; set; }
        public Subject? Subject { get; set; }
        public List<Teacher>? Teachers { get; set; }
    }
}
