using LoginHrSystems.DTOs.Roles;
using LoginHrSystems.Models.Roles;
using LoginHrSystems.Repositories.UnitOfWork;
using LoginHrSystems.Services.Contract;

namespace LoginHrSystems.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _uow;

        public RoleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task AddRoleAsync(AddingRoleDto dto)
        {
            var role = new Role { 
                Name = dto.Name, 
                RolePermissions = new List<RolePermission>() 
            };

            await _uow.Roles.AddAsync(role);
            await _uow.SaveAsync();
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return (List<Role>)await _uow.Roles.GetAllAsync();
        }

        public async Task UpdateRolePermissionsAsync(UpdateRolePermissionsDto dto)
        {
            var role = await _uow.Roles.GetByIdAsync(dto.RoleId);

            if (role == null) 
                throw new Exception("Role not found");

            var permissions = await _uow.Permissions.GetByIdsAsync(dto.PermissionIds);

            role.RolePermissions = permissions.Select(
                p => new RolePermission
                {
                    PermissionId = p.Id,
                }
            ).ToList();

            await _uow.SaveAsync();
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _uow.Roles.GetByIdAsync(roleId);
            if (role != null)
            {
                _uow.Roles.Delete(role);
                await _uow.SaveAsync();
            }
        }
    }
}
