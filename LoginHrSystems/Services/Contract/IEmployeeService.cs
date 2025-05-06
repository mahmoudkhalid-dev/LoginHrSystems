using LoginHrSystems.DTOs.Employees;
using LoginHrSystems.Models.Employees;

namespace LoginHrSystems.Services.Contract
{
    public interface IEmployeeService
    {
        Task AddEmployeeAsync(AddingEmployeeDto dto);
        Task UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeDetailsAsync(int id);
        Task DeleteEmployeeAsync(int id);
    }
}
