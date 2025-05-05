using System.ComponentModel.DataAnnotations.Schema;

namespace LoginHrSystems.Models.Roles
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;

        public int PermissionId { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; } = null!;
    }
}
