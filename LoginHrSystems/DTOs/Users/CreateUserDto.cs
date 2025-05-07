namespace LoginHrSystems.DTOs.Users
{
    public class CreateUserDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<int> RoleIds { get; set; } = null!;
    }
}
