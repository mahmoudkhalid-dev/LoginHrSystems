using LoginHrSystems.Data;
using LoginHrSystems.Models.Employees;
using LoginHrSystems.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoginHrSystems.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(
            bool applyNameFilter,
            string? name,
            bool applyJobTitleFilter,
            string? jobTitle,
            bool applySalaryRange,
            double salaryFrom,
            double salaryTo
        )
        {

            var employees = _context.Employees.Where(e => e.IsActive && !e.IsDeleted);

            if (applyNameFilter)
            {
                employees = employees.Where(e => e.Name.Contains(name));
            }

            if (applyJobTitleFilter)
            {
                employees = employees.Where(e => e.JobTitle.Contains(jobTitle));
            }

            if (applySalaryRange)
            {
                employees = employees.Where(e => e.Salary >= salaryFrom && e.Salary <= salaryTo);
            }

            return await employees.ToListAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees.Where(e => !e.IsActive).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

    }
}
