using Lezione6.SchoolManager.Data;

namespace Lezione6.SchoolManager.DTO
{
    public class Mapper
    {
        public StudentDto MapEntityToDto(Student entity)
        {
            StudentDto dto = new StudentDto()
            {
                Name = entity.Name,
                Surname = entity.Surname,
                Id = entity.StudentId,
                Courses = entity.Enrollments?
                            .Where(e => e.Course != null)
                            .Select(e => e.Course)
                            .ToList().ConvertAll(MapEntityToDto),
            };
            return dto;
        }

        public CourseDto MapEntityToDto(Course entity)
        {
            CourseDto dto = new CourseDto()
            {
                Title = entity.Title,
                Id = entity.CourseId
            };
            return dto;
        }

        public Student MapDtoToEntity(StudentDto dto)
        {
            return new Student()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                StudentId = dto.Id
            };
        }
    }
}
