namespace LoginHrSystems.DTOs.Users
{
    public class EditUserPermissionsDto
    {
        public int UserId { get; set; }
        public List<int> PermissionIds { get; set; } = null!;
    }
}
