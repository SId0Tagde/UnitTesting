using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Employeemanagement.DatabaseBuild
{
    public class AppRepository : IAppRepository
    {

        private readonly AppDbContext context;
        private readonly ILogger<AppRepository> logger;

        public AppRepository(AppDbContext context, ILogger<AppRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        //General
        public void Add<T>(T entity) where T : class
        {
            logger.LogInformation($"Adding object of type {entity.GetType}");
            context.Add<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            logger.LogInformation($"Removing an object of type {entity.GetType}");
            context.Remove<T>(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            logger.LogInformation($"Attempting to save changes");
            return ((await context.SaveChangesAsync()) > 0);
        }

        //Department
        public async Task<bool> existsDepartment(int departmentId)
        {
            logger.LogInformation($"Checking if {departmentId} exists");
            return await context.Departments
                                .Where(dp => dp.Id == departmentId)
                                .AnyAsync();
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            logger.LogInformation($"Getting Department Object with {departmentId}");
            return await context.Departments
                            .Where(dp => dp.Id == departmentId)
                            .FirstAsync();
        }

        //Employee
        public async Task<bool> existsEmployee(int empid)
        {
            logger.LogInformation($"Checking if {empid} exists");
            return await context.Employees
                                 .Where(emp => emp.Id == empid)
                                 .AnyAsync();
        }

        public async Task<Employee> GetEmployee(int empid)
        {
            logger.LogInformation($"Getting Employee having id={empid}");
            return await  context.Employees.Include(emp => emp.Department)
                                .Where(emp => emp.Id == empid)
                                .FirstAsync();
        }

        public async Task<Employee[]> GetDepartmentEmployeesFrom(int departmentId)
        {
            logger.LogInformation($"Getting Employees from department: {departmentId}");
            return await context.Employees
                                .Where(emp => emp.Department.Id == departmentId)
                                .ToArrayAsync();
        }
    }
}
