namespace Lezione6.SchoolManager.Data
{
    public record class Evaluation : BaseEntity
    {
        public int EvaluationId { get; set; }
        public int Value { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
