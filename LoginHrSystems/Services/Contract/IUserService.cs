using LoginHrSystems.DTOs.Users;

namespace LoginHrSystems.Services.Contract
{
    public interface IUserService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task CreateUserAsync(CreateUserDto dto);
        Task ChangeUserRoleAsync(ChangeUserRoleDto dto);
        Task<List<string>> GetUserPermissionsAsync(int userId);
        Task EditUserPermissionsAsync(EditUserPermissionsDto dto);
    }
}
