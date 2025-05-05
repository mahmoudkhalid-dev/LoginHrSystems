using LoginHrSystems.Models.Users;

namespace LoginHrSystems.Models.Roles
{
    public class Role
    {

        public Role()
        {
            UserRoles = new List<UserRole>();
            RolePermissions = new List<RolePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Navigations
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

    }
}
