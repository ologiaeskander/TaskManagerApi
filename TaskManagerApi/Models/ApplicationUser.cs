using Microsoft.AspNetCore.Identity;

namespace YourProjectName.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public bool? IsActive { get; set; }

        public byte[]? ProfilePicture { get; set; }
    }
}