using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SynetecAssessment.Api.Controllers;
using SynetecAssessment.Contract.Model;
using SynetecAssessment.Core.Interface;
using SynetecAssessment.Core.Services;
using SynetecAssessment.Data;
using SynetecAssessment.Data.Interface;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Test
{
    [TestFixture]
    public class ApiTests
    {
        private BonusPoolController controller;
        private IDbContextGenerator _contextFactory;
        private IBonusPoolService _bounusPoolService;

        [SetUp]
        public void Setup()
        {
            _contextFactory = new DbContextGenerator();
            _bounusPoolService = new BonusPoolService(_contextFactory);
            controller = new BonusPoolController(_bounusPoolService);
         }

        [Test]
        public async Task GivenCheckGetAllReturnsValidStatusCode()
        {
            //Arrange and Act
            var result = await controller.GetAll();
            var response = result as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }


        [Test]
        public async Task GivenSelectedEmpoyeeIDisNotpassedValidErrorMessageisDisplayed()
        {

            //Arrange and Act
            var reqWithoutSelectedEmployeeId = new CalculateBonusDto();
            reqWithoutSelectedEmployeeId.TotalBonusPoolAmount = 5000;
            var result = await controller.CalculateBonus(reqWithoutSelectedEmployeeId);
            var response = result as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.AreEqual("SelectedEmployeeId is not specified", response.Value);
            Assert.AreEqual(404,response.StatusCode);
        }
 
        [TestCase(1, 458)]
        [TestCase(2, 687)]
        [TestCase(3, 725)]
        [TestCase(4, 420)]
        [TestCase(5, 343)]
        [TestCase(6, 267)]
        [TestCase(7, 477)]
        [TestCase(8, 295)]
        [TestCase(9, 274)]
        [TestCase(10, 278)]
        [TestCase(11, 404)]
        [TestCase(12, 366)]
        public async Task GivenBonusCalculatedReturnedForEmployees(int employeeId, int amount)
        {
            //Arrange 
            var req =  new CalculateBonusDto();
            req.TotalBonusPoolAmount = 5000;
            req.SelectedEmployeeId = employeeId;

            // Act
            var result = await controller.CalculateBonus(req).ConfigureAwait(true);
            var response = result as ObjectResult;
            var respValue = response.Value as BonusPoolCalculatorResultDto;

            //Assert
            Assert.NotNull(response);
            Assert.AreEqual(amount, respValue.Amount);

        }


        [Test]
        public async Task GivenForTheNotValidEmployeesErrorMessageIsReturned()
        {
            //Arrange and Act
            var reqWithNotValidEmployeeId = new CalculateBonusDto();
            reqWithNotValidEmployeeId.TotalBonusPoolAmount = 5000;
            reqWithNotValidEmployeeId.SelectedEmployeeId = 17;
            var result = await controller.CalculateBonus(reqWithNotValidEmployeeId);
            var response = result as ObjectResult;

            //Assert
            Assert.NotNull(response);
            Assert.AreEqual("Employee not found", response.Value);
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}