namespace LoginHrSystems.DTOs.Users
{
    public class GetUserPermissionsDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public List<string> Permissions { get; set; } = null!;

    }
}
