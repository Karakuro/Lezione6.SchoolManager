using System.ComponentModel.DataAnnotations;

namespace Lezione6.SchoolManager.Data
{
    public record class Student : BaseEntity
    {
        [Key]
        public int StudentId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        public List<Evaluation>? Evaluations { get; set; }
    }
}
