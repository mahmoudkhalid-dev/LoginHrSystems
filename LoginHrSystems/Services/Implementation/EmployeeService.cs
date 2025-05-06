using LoginHrSystems.DTOs.Employees;
using LoginHrSystems.Models.Employees;
using LoginHrSystems.Repositories.UnitOfWork;
using LoginHrSystems.Services.Contract;

namespace LoginHrSystems.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;

        public EmployeeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task AddEmployeeAsync(AddingEmployeeDto dto)
        {
            var emp = new Employee
            {
                Name = dto.Name,
                Department = dto.Department
            };

            await _uow.Employees.AddAsync(emp);
            await _uow.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var emp = await _uow.Employees.GetByIdAsync(id);

            if (emp == null)
                throw new Exception("Not found");

            emp.Name = dto.Name;
            emp.Department = dto.Department;
            await _uow.SaveAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return (List<Employee>)await _uow.Employees.GetAllAsync();
        }

        public async Task<Employee?> GetEmployeeDetailsAsync(int id)
        {
            return await _uow.Employees.GetByIdAsync(id);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var emp = await _uow.Employees.GetByIdAsync(id);

            if (emp != null)
            {
                _uow.Employees.Delete(emp);
                await _uow.SaveAsync();
            }
        }
    }
}
