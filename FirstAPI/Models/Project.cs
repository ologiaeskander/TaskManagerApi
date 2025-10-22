﻿using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
