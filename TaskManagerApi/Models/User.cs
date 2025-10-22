using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public enum Role { Manager, Employee}
    public enum Department { Technical, Marketing, HR}
    public class User
    {
        public int Id { get; set; }
        public required string FullName { get; set; } = string.Empty;
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty; //todo hash this later
        public required string Email { get; set; } = string.Empty;
        public int Phone { get; set; }
        public Role Role {get; set;} 
        public Department Department { get; set;}
    }
}
