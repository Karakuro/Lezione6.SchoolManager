namespace Lezione6.SchoolManager.DTO
{
    public class CourseDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<StudentDto>? Students { get; set; }
    }
}
