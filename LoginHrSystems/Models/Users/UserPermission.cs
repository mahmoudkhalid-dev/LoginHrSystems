using LoginHrSystems.Models.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginHrSystems.Models.Users
{
    public class UserPermission
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public int PermissionId { get; set; }

        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; } = null!;
    }
}
