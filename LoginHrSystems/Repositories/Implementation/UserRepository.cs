using LoginHrSystems.Data;
using LoginHrSystems.Models.Users;
using LoginHrSystems.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoginHrSystems.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.UserPermissions)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.UserPermissions)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public async Task<List<string>> GetUserPermissionsAsync(int userId)
        {
            var rolePermissions = await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.Name))
                .ToListAsync();

            var directPermissions = await _context.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => up.Permission.Name)
                .ToListAsync();

            return rolePermissions.Concat(directPermissions).Distinct().ToList();
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var entity = new UserRole { UserId = userId, RoleId = roleId };
            await _context.UserRoles.AddAsync(entity);
        }

        public async Task RemoveRolesAsync(int userId)
        {
            var roles = _context.UserRoles.Where(ur => ur.UserId == userId);
            _context.UserRoles.RemoveRange(roles);
        }

        public async Task AssignPermissionsAsync(int userId, List<int> permissionIds)
        {
            var permissions = permissionIds.Select(id => new UserPermission
            {
                UserId = userId,
                PermissionId = id
            });

            await _context.UserPermissions.AddRangeAsync(permissions);
        }

        public async Task RemovePermissionsAsync(int userId)
        {
            var userPerms = _context.UserPermissions.Where(up => up.UserId == userId);
            _context.UserPermissions.RemoveRange(userPerms);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
