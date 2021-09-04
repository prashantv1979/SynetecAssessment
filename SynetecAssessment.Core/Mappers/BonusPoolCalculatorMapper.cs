using SynetecAssessment.Contract.Model;
using SynetecAssessment.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessment.Core.Mappers
{
    public static class BonusPoolCalculatorMapper
    {
        public static BonusPoolCalculatorResultDto MapBonusPoolCalculatorResult(int bonusAllocation, Employee employee)
        {
            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto
                {
                    FullName = employee.Fullname,
                    JobTitle = employee.JobTitle,
                    Salary = employee.Salary,
                    Department = new DepartmentDto
                    {
                        Title = employee.Department.Title,
                        Description = employee.Department.Description
                    }
                },

                Amount = bonusAllocation
            };
        }
    }
}
