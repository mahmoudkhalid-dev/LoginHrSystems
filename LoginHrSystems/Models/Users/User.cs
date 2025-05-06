namespace LoginHrSystems.Models.Users
{
    public class User
    {

        public User()
        {
            UserRoles = new List<UserRole>();
            UserPermissions = new List<UserPermission>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; }
        public string HashedPassword { get; set; }

        // Navigations
        public List<UserRole> UserRoles { get; set; }
        public List<UserPermission> UserPermissions { get; set; }

    }
}
