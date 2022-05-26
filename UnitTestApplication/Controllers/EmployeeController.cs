using AutoMapper;
using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;

namespace Employeemanagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly IAppRepository Repository;
        //private readonly ILogger<EmployeeController> Logger;
        //private readonly LinkGenerator linkGenerator;

        public EmployeeController( IAppRepository repository)
        {
            //Logger = logger;
            Repository = repository;
            //this.linkGenerator = linkGenerator;
        }

        /// <summary>
        /// Returns the Employee from EmployeeId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> Get(int employeeId)
        {
            if(await Repository.existsEmployee(employeeId))
            {
                var emp = await Repository.GetEmployee(employeeId);
                return Ok(emp);
            }
            return NotFound($"Employee with {employeeId} not exists");
        }

        /// <summary>
        /// Post a Employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> Post([FromBody] Employee model)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(model);
            }
            
            //Does employee exists
            if (await Repository.existsEmployee(model.Id))
            {
                return BadRequest($"EmployeeId : {model.Id} already exists");
            }

            if(string.IsNullOrWhiteSpace(model.Department.DepartmentName) )
            {
                throw new InvalidDataException($"{model.Department}");
            }

            Employee emp;
            if (await Repository.existsDepartment(model.Department.Id))
            {
                var dpmnt = await Repository.GetDepartment(model.Department.Id);

                emp = new Employee()
                {
                    Id = model.Id,
                    Name = model.Name,
                    SurName = model.SurName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Qualification = model.Qualification,
                    Department = dpmnt
                };

                Repository.Add(emp);

            }
            else
            {
                emp = new Employee()
                {
                    Id = model.Id,
                    Name = model.Name,
                    SurName = model.SurName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Qualification = model.Qualification,
                    Department = model.Department
                };

                Repository.Add(emp);
                
            }

            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/employee/{emp.Id}", emp);
            }
            else
            {
                return BadRequest("Unable to Save");
            }

        }

    }
}