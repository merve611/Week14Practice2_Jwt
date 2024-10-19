using System.ComponentModel.DataAnnotations;

namespace Week14Practice2_Jwt.Models
{
    public class LoginRequest
    {

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
