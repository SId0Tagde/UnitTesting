using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Employeemanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IAppRepository Repository;

        public DepartmentController(IAppRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gives Employees of Department
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        // GET: api/<DepartmentController>
        [HttpGet("{departmentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee[]))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee[]>> Get(int departmentId)
        {
            if(await Repository.existsDepartment(departmentId))
            {
                return Ok(await Repository.GetDepartmentEmployeesFrom(departmentId));
            }
            return NotFound($"Department:{departmentId} does not exist");
        }

        
    }
}
