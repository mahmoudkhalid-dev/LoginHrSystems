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
                Department = dto.Department,
                Address = dto.Address,
                Bonus = dto.Bonus,
                BonusCurrency = dto.BonusCurrency,
                Code = dto.Code,
                CreatedAt = DateTime.Now,
                Email = dto.Email,
                IsActive = true,
                IsDeleted = false,
                JobTitle = dto.JobTitle,
                Phone = dto.Phone,
                Salary = dto.Salary,
                SalaryCurrency = dto.SalaryCurrency,
                YearsOfExperience = dto.YearsOfExperience,
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

        public async Task<List<GetEmployeeDto>> GetEmployeesAsync(
            bool applyNameFilter,
            string? name,
            bool applyJobTitleFilter,
            string? jobTitle,
            bool applySalaryRange,
            double salaryFrom,
            double salaryTo,
            bool isDetailed
        )
        {
            var employees = await _uow.Employees.GetAllAsync(
                applyNameFilter,
                name,
                applyJobTitleFilter,
                jobTitle,
                applySalaryRange,
                salaryFrom,
                salaryTo
            );

            var res = employees.Select(e => new GetEmployeeDto
            {
                Address = e.Address,
                Code = e.Code,
                Department = e.Department,
                Email = e.Email,
                Id = e.Id,
                JobTitle = e.JobTitle,
                Name = e.Name,
                Phone = e.Phone,
                YearsOfExperience = e.YearsOfExperience,
                SalaryDetails = isDetailed ? new SalaryDetailsDto
                {
                    Bonus = e.Bonus,
                    BonusCurrency = e.BonusCurrency,
                    Salary = e.Salary,
                    SalaryCurrency = e.SalaryCurrency
                } : null
            }).ToList();


            return res;
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
