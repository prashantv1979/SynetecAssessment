using Microsoft.AspNetCore.Mvc;
using SynetecAssessment.Contract.Model;
using SynetecAssessment.Core.Interface;
using System.Threading.Tasks;

namespace SynetecAssessment.Api.Controllers
{
    [Route("api/")]
    public class BonusPoolController : Controller
    {
        private readonly  IBonusPoolService _bonusPoolService;
        public BonusPoolController(IBonusPoolService bonusPoolService)
        {
            _bonusPoolService = bonusPoolService;
        }
        [HttpGet]
        [Route("getEmployeesDetails")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _bonusPoolService.GetEmployeesAsync());
            
        }

        [HttpPost()]
        [Route("bonusCalculator")]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {

           if (request ==null ||request.SelectedEmployeeId ==0 || request.SelectedEmployeeId.ToString()==null)
            {
                var message = "SelectedEmployeeId is not specified";
                return NotFound(message);
            }

            var response = await _bonusPoolService.CalculateAsync(
               request.TotalBonusPoolAmount,
               request.SelectedEmployeeId);

            if (response==null)
            {
               var message = "Employee not found";
              return NotFound(message);
            }

            return Ok(response);
    
        }
    }
}
