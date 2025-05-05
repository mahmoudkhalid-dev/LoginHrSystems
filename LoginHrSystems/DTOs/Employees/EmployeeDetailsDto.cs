namespace LoginHrSystems.DTOs.Employees
{
    public class EmployeeDetailsDto
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Department { get; set; } = null!;
        public int YearsOfExperience { get; set; }
        public SalaryDetailsDto SalaryDetails { get; set; } = null!;
    }
}
