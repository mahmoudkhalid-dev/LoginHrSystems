using LoginHrSystems.Models.Roles;

namespace LoginHrSystems.Repositories.Contract
{
    public interface IPermissionRepository
    {
        Task<List<Permission>> GetByIdsAsync(List<int> ids);
    }
}
