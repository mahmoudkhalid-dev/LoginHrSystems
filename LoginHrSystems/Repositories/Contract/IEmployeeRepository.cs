using LoginHrSystems.Models.Employees;

namespace LoginHrSystems.Repositories.Contract
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task<Employee?> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        void Remove(Employee employee);
    }
}
