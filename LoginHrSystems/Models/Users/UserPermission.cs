﻿using LoginHrSystems.Models.Roles;

namespace LoginHrSystems.Models.Users
{
    public class UserPermission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
