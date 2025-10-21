using System.ComponentModel.DataAnnotations;

namespace FirstAPI.Models
{
    public enum Role { Manager, Employee}
    public enum Department { Technical, Marketing, HR}
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; }
        public int Phone { get; set; }
        public Role Role {get; set;} 
        public Department Department { get; set;}
    }
}
