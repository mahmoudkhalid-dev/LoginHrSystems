namespace LoginHrSystems.DTOs.Employees
{
    public class SalaryDetailsDto
    {
        public double Salary { get; set; }
        public string SalaryCurrency { get; set; } = null!;
        public double Bonus { get; set; }
        public string BonusCurrency { get; set; } = null!;
    }
}
