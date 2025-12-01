using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class LoginModel
    {
        [Required]
        public required string EmailOrUserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
