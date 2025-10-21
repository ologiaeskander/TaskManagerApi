using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Models
{
    public enum Status { ToDo, InProgress, Done }
    public enum Priority { Low, Medium, High }

    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public int AssignedToUserId { get; set; }
        public int? ProjectId { get; set; } //nullable in case of rogue/general tasks
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; } 
        public int CreatedBy { get; set; }
    }
}
