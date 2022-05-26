using Employeemanagement.Controllers;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class DepartmentTests
    {
        private readonly IAppRepository Repository;
        private readonly DepartmentController Controller;

        public DepartmentTests()
        {
            Repository = new MockAppRepository();
            Controller = new DepartmentController(Repository);
        }

        [Fact]
        public async Task GetDepartmentEmployees()
        {
            //Arrange
            var actionResult = await Controller.Get(234);

            //Assert
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.IsType<OkObjectResult>(okObjectResult);

            var employeeArray = okObjectResult?.Value as Employee[];
            Assert.IsType<Employee[]>(employeeArray);
        }

        [Fact]
        public async Task IfDepartmentDoesNoTExist_ThrowsNotFound()
        {
            //Act
            var actionResult = await Controller.Get(345);

            //Assert
            var notfound = actionResult.Result as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(notfound);

        }
    }
}
