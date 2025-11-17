using Microsoft.AspNetCore.Identity;

namespace YourProjectName.Models
{
    public enum Role {
        SuperAdmin,
        Admin,
        Basic
    }
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role Role { get; set; } = Role.Basic;
        public byte[]? ProfilePicture { get; set; }
    }
}