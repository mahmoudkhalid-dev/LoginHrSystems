using LoginHrSystems.DTOs.Roles;
using LoginHrSystems.Models.Roles;

namespace LoginHrSystems.Services.Contract
{
    public interface IRoleService
    {
        Task AddRoleAsync(AddingRoleDto dto);
        Task<List<Role>> GetAllRolesAsync();
        Task UpdateRolePermissionsAsync(UpdateRolePermissionsDto dto);
        Task DeleteRoleAsync(int roleId);
    }
}
