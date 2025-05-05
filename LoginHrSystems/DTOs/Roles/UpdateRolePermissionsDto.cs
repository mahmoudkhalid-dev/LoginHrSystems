namespace LoginHrSystems.DTOs.Roles
{
    public class UpdateRolePermissionsDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = null!;
    }
}
