using LoginHrSystems.Models.Users;

namespace LoginHrSystems.Repositories.Contract
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
    }
}
