using LoginHrSystems.DTOs.Employees;
using LoginHrSystems.Models.Employees;

namespace LoginHrSystems.Services.Contract
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(AddingEmployeeDto dto);
        Task UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
        Task<List<GetEmployeeDto>> GetEmployeesAsync(
            bool applyNameFilter,
            string? name,
            bool applyJobTitleFilter,
            string? jobTitle,
            bool applySalaryRange,
            double salaryFrom,
            double salaryTo,
            bool isDetailed
        );
        Task<Employee?> GetEmployeeDetailsAsync(int id);
        Task DeleteEmployeeAsync(int id);
    }
}
