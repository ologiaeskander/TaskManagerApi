using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerApi.Data;

namespace TaskManagerApi.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [ForeignKey("Creator")]
        [Required]
        public required string CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
