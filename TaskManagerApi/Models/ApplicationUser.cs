using Microsoft.AspNetCore.Identity;
using static TaskManagerApi.Models.Authorization;

namespace TaskManagerApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Roles Role { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}