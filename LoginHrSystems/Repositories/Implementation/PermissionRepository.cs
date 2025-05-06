using LoginHrSystems.Data;
using LoginHrSystems.Models.Roles;
using LoginHrSystems.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoginHrSystems.Repositories.Implementation
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _context;

        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<List<Permission>> GetByIdsAsync(List<int> ids)
        {
            return _context.Permissions.Where(p => ids.Contains(p.Id)).ToListAsync();
        }
    }
}
