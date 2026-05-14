using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lezione6.SchoolManager.Data
{
    [PrimaryKey(nameof(TeacherId), nameof(ModuleId))]
    public record class Assignment : BaseEntity
    {
        public int TeacherId { get; set; }
        public int ModuleId { get; set; }
        public int AssignedHours { get; set; }
        public Teacher? Teacher { get; set; }
        public Module? Module { get; set; }
    }
}
