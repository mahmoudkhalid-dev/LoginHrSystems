namespace LoginHrSystems.Models.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Department { get; set; } = null!;
        public int YearsOfExperience { get; set; }
        public double Salary { get; set; }
    }
}
