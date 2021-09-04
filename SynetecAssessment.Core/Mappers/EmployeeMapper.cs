using SynetecAssessment.Contract.Model;
using SynetecAssessment.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace SynetecAssessment.Core.Mappers
{
    public static class EmployeeMapper
    {
        private static EmployeeDto employeeResult;

        public static List<EmployeeDto> MapEmployee(IEnumerable<Employee> employees)
        {
            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        FullName = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }
            return result;     
        }

    }
}
