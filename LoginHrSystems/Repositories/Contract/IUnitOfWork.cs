namespace LoginHrSystems.Repositories.Contract
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IPermissionRepository Permissions { get; }
        IEmployeeRepository Employees { get; }
        Task<int> SaveAsync();
    }
}
