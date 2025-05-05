namespace LoginHrSystems.DTOs.Users
{
    public class ChangeUserRoleDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = null!;
    }
}
