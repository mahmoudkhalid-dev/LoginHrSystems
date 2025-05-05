using LoginHrSystems.Models.Roles;

namespace LoginHrSystems.Repositories.Contract
{
    public interface IRoleRepository
    {
        Task<Role?> GetByIdAsync(int id);
        Task<List<Role>> GetAllAsync();
        Task AddAsync(Role role);
        void Remove(Role role);
    }
}
