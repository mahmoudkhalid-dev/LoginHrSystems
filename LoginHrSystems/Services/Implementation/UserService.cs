using LoginHrSystems.DTOs.Users;
using LoginHrSystems.Helpers;
using LoginHrSystems.Models.Roles;
using LoginHrSystems.Models.Users;
using LoginHrSystems.Repositories.UnitOfWork;
using LoginHrSystems.Services.Contract;
using System.Collections.Generic;

namespace LoginHrSystems.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly JwtHelper _jwt;

        public UserService(IUnitOfWork uow, JwtHelper jwt)
        {
            _uow = uow;
            _jwt = jwt;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _uow.Users.GetByUsernameAsync(dto.UserName);

            if (user == null || user.HashedPassword != dto.Password)
                throw new UnauthorizedAccessException();

            var permissions = user.UserPermissions.Select(p => p.Permission.Name.ToString());

            return _jwt.GenerateToken(user, permissions);
        }

        public async Task CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                UserName = dto.Username,
                HashedPassword = dto.Password,
                UserRoles = new List<UserRole>()
            };

            foreach (var roleId in dto.RoleIds)
            {
                var role = await _uow.Roles.GetByIdAsync(roleId);

                if (role != null)
                {
                    user.UserRoles.Add(
                        new UserRole
                        {
                            RoleId = roleId,
                        }
                    );
                }

            }

            await _uow.Users.AddAsync(user);
            await _uow.SaveAsync();
        }

        public async Task ChangeUserRoleAsync(ChangeUserRoleDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(dto.UserId);

            if (user == null)
                throw new Exception("User not found");

            user.UserRoles.Clear();

            foreach (var roleId in dto.RoleIds)
            {
                var role = await _uow.Roles.GetByIdAsync(roleId);

                if (role != null)
                    user.UserRoles.Add(
                        new UserRole
                        {
                            RoleId = roleId,
                        }
                    );
            }

            await _uow.SaveAsync();
        }

        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {
            var user = await _uow.Users.GetByIdAsync(userId);

            if (user == null) return new();

            var res = user.UserRoles.SelectMany(r => r.Role.RolePermissions)
                                    .Select(p => p.Permission.Name)
                                    .Distinct()
                                    .ToList();

            return res;
        }

        public async Task EditUserPermissionsAsync(EditUserPermissionsDto dto)
        {
            var user = await _uow.Users.GetByIdAsync(dto.UserId);

            if (user == null) throw new Exception("User not found");

            var permissions = await _uow.Permissions.GetByIdsAsync(dto.PermissionIds);

            user.UserPermissions.AddRange(
                permissions.Select(
                    p => new UserPermission
                    {
                        PermissionId = p.Id
                    }
                )
            );

            await _uow.SaveAsync();
        }
    }
}
