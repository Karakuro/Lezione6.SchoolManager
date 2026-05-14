using Microsoft.EntityFrameworkCore;

namespace Lezione6.SchoolManager.Data
{
    public record class Module : BaseEntity
    {
        public int ModuleId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public required string Title { get; set; }
        public required int Hours { get; set; }
        public Course? Course { get; set; }
        public Subject? Subject { get; set; }
        public List<Assignment>? Assignments { get; set; }
    }
}

//dotnet ef migrations add ModuleHours
//dotnet ef database update
