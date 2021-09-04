using Microsoft.EntityFrameworkCore;
using SynetecAssessment.Contract.Model;
using SynetecAssessment.Core.Interface;
using SynetecAssessment.Core.Mappers;
using SynetecAssessment.Data.Interface;
using SynetecAssessment.Data.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessment.Core.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IDbContextGenerator _contextFactory;
     
        public BonusPoolService(IDbContextGenerator contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            using (var _dbContext = _contextFactory.Create())
            {
                IEnumerable<Employee> employees = await _dbContext
                     .Employees
                     .Include(e => e.Department)
                     .ToListAsync();
                return EmployeeMapper.MapEmployee(employees);
            }

        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            using (var _dbContext = _contextFactory.Create())
            {
                //load the details of the selected employee using the Id
                Employee employee = await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);

                if (employee == null)
                {
                    return null;
                }

                //get the total salary budget for the company
                int totalSalary = (int)_dbContext.Employees.Sum(item => item.Salary);

                //calculate the bonus allocation for the employee
                decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
                int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

                return BonusPoolCalculatorMapper.MapBonusPoolCalculatorResult(bonusAllocation, employee);
            }
        }

    }
}
