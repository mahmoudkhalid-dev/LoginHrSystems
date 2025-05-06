using LoginHrSystems.Models.Roles;

namespace LoginHrSystems.Repositories.Contract
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        void Delete(Role role);
        Task AddAsync(Role role);
        Task RemovePermissionsAsync(int roleId);
    }
}
