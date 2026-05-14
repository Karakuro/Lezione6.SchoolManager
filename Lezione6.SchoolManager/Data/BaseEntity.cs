namespace Lezione6.SchoolManager.Data
{
    public abstract record class BaseEntity
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
