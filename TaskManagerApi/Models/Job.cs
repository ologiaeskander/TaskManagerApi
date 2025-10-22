using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public enum Status { ToDo, InProgress, Done }
    public enum Priority { Low, Medium, High }

    public class Job
    {
        public int Id { get; set; }
        public required string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public int AssignedToUserId { get; set; }
        public int? ProjectId { get; set; } //nullable in case of rogue/general jobs
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; } 
        public int CreatedBy { get; set; }
    }
}
