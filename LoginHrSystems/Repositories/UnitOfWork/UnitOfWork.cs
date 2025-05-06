using LoginHrSystems.Data;
using LoginHrSystems.Repositories.Contract;
using System;

namespace LoginHrSystems.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _context;

        public IUserRepository Users { get; }
        public IRoleRepository Roles { get; }
        public IPermissionRepository Permissions { get; }
        public IEmployeeRepository Employees { get; }

        public UnitOfWork(ApplicationDbContext context,
                          IUserRepository users,
                          IRoleRepository roles,
                          IPermissionRepository permissions,
                          IEmployeeRepository employees)
        {
            _context = context;
            Users = users;
            Roles = roles;
            Permissions = permissions;
            Employees = employees;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
