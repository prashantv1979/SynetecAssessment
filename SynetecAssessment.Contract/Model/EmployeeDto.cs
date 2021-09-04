namespace SynetecAssessment.Contract.Model
{
    public class EmployeeDto
    {
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
