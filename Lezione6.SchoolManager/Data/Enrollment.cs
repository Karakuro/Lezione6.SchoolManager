using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lezione6.SchoolManager.Data
{
    [PrimaryKey(nameof(StudentId), nameof(CourseId))]
    public class Enrollment
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public required DateTime Date { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }
    }
}
