using LoginHrSystems.Models.Employees;

namespace LoginHrSystems.Repositories.Contract
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync(
            bool applyNameFilter,
            string? name,
            bool applyJobTitleFilter,
            string? jobTitle,
            bool applySalaryRange,
            double salaryFrom,
            double salaryTo
        );
        public void Delete(Employee employee);
    }
}
