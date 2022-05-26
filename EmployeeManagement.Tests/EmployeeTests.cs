using AutoMapper;
using Employeemanagement.Controllers;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Tests
{
    public class EmployeeTests 
    {
        private readonly EmployeeController controller;
        private readonly IAppRepository appRepository;

        public EmployeeTests()
        {
            appRepository = new MockAppRepository();
            controller = new EmployeeController(appRepository);
        }

        [Fact]
        public async Task GetEmployee()
        {
            //Act
            var actionResult = await controller.Get(1);

            //Assert
            var okobjectresult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okobjectresult);
            Assert.IsType<OkObjectResult>(okobjectresult);

            var employee = okobjectresult?.Value as Employee;
            Assert.NotNull(employee);
            Assert.Equal(1, employee?.Id);
        }

        [Fact]
        public async Task AddEmployeeWithDepartment()
        {
            //Arrange
            var department = new Department()
            {
                Id = 23,
                DepartmentName = "Adhyaay"
            };

            var employee = new Employee()
            {
                Id = 245,
                Name = "Dhanjay",
                SurName = "Veer",
                Address = "Behind Sahiba Chowk,Guru Darbaar",
                Department = department,
                PhoneNumber = "3453452314",
                Qualification = "tanjnya"
            };

            //Act
            var actionResponse = await controller.Post(employee);

            //Assert
            var createdResult = actionResponse.Result as CreatedResult;
            Assert.IsType<CreatedResult>(createdResult);
        }

        [Fact]
        public void AddingEmployeeWithoutDepartment_ThrowsInvalidDataException()
        {
            //Arrange
            var department = new Department()
            {
                Id = 23,
                //DepartmentName = "  "
                //DepartmentName = null
                DepartmentName = ""
            };
            var employee = new Employee()
            {
                Id = 23,
                Name = "Dhanjay",
                SurName = "Veer",
                Department = department,
                Address = "Behind Sahiba Chowk,Guru Darbaar",
                PhoneNumber = "3453452314",
                Qualification = "tanjnya"
            };

            //Act

            //Assert
            Assert.ThrowsAsync<InvalidDataException>(
                    async() => await controller.Post(employee));
        }

    }
}