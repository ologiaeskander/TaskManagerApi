using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models
{
    public class TokenRequestModel
    {
        [Required]
        public required string EmailOrUserName { get; set; }
        //public string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
