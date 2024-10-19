namespace Week14Practice2_Jwt.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
    public enum UserType
    {
        User, Admin
    }
}
