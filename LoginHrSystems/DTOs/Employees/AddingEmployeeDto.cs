namespace LoginHrSystems.DTOs.Employees
{
    public class AddingEmployeeDto
    {
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Department { get; set; } = null!;
        public int YearsOfExperience { get; set; }
        public double Salary { get; set; }
        public string SalaryCurrency { get; set; } = null!;
        public double Bonus { get; set; }
        public string BonusCurrency { get; set; } = null!;
    }
}
