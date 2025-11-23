using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class TokenRequestModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
