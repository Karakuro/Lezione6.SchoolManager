namespace Lezione6.SchoolManager.Data
{
    public record class Teacher : BaseEntity
    {
        public int TeacherId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public int YearFirstEmployed { get; set; }
        public List<Assignment>? Assignments { get; set; }
        public List<Subject>? Subjects { get; set; }
    }
}
