using System.ComponentModel.DataAnnotations;
using TaskManagerApi.Data;

namespace TaskManagerApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
