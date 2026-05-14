namespace Lezione6.SchoolManager.Data
{
    public record class Subject : BaseEntity
    {
        public int SubjectId { get; set; }
        public required string Name { get; set; }
        public List<Teacher>? Teachers { get; set; }
        public List<Module>? Modules { get; set; }
    }
}
