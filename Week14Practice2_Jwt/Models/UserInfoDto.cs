using Week14Practice2_Jwt.Entities;

namespace Week14Practice2_Jwt.Models
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
    }
}
