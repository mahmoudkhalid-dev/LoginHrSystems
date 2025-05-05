using System.ComponentModel.DataAnnotations.Schema;
using LoginHrSystems.Models.Roles;

namespace LoginHrSystems.Models.Users
{
    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;

    }
}
