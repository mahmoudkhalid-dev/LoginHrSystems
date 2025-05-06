using LoginHrSystems.Data;
using LoginHrSystems.Models.Roles;
using LoginHrSystems.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoginHrSystems.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public void Delete(Role role)
        {
            _context.Roles.Remove(role);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _context.Roles.Include(r => r.RolePermissions)
                                       .ThenInclude(rp => rp.Permission)
                                       .FirstOrDefaultAsync(r => r.Id == id);
        }

        public void Update(Role role)
        {
            _context.Roles.Update(role);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _context.Roles.AnyAsync(r => r.Name == roleName);
        }

        public async Task AssignPermissionsAsync(int roleId, List<int> permissionIds)
        {
            var entities = permissionIds.Select(p => new RolePermission
            {
                RoleId = roleId,
                PermissionId = p
            });

            await _context.RolePermissions.AddRangeAsync(entities);
        }

        public async Task RemovePermissionsAsync(int roleId)
        {
            var perms = _context.RolePermissions.Where(rp => rp.RoleId == roleId);
            _context.RolePermissions.RemoveRange(perms);
        }

        public async Task<List<string>> GetRolePermissionsAsync(int roleId)
        {
            return await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.Permission.Name)
                .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
