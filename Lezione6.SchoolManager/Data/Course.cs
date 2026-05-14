namespace Lezione6.SchoolManager.Data
{
    public record class Course : BaseEntity
    {
        public int CourseId { get; set; }
        public required string Title { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        public List<Module>? Modules { get; set; }
    }
}
