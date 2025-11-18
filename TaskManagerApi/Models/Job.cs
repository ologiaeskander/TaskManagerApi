using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TaskManagerApi.Models
{
    public enum Status { ToDo, InProgress, Done }
    public enum Priority { Low, Medium, High }

    public class Job
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        //Relationships
        [ForeignKey("AssignedToUser")]
        public string? AssignedToUserId { get; set; }
        public ApplicationUser? AssignedToUser { get; set; }
        [ForeignKey("Project")]
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }

        [ForeignKey("Creator")]
        [Required]
        public required string CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }
    }
}
