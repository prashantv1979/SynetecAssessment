using SynetecAssessment.Contract.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessment.Core.Interface
{
    public interface IBonusPoolService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId);
    }
}
