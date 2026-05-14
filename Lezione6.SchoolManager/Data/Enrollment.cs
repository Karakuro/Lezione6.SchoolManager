using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lezione6.SchoolManager.Data
{
    [PrimaryKey(nameof(StudentId), nameof(CourseId))]
    public record class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public required DateTimeOffset Date { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }
    }
}
