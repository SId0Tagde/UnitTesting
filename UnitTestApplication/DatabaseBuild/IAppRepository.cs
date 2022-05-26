using Employeemanagement.DatabaseBuild.EntityModels;

namespace Employeemanagement.DatabaseBuild
{
    public interface IAppRepository
    {
        //General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();

        //Employee
        Task<Employee> GetEmployee(int empid);
        Task<bool> existsEmployee(int empid);
        Task<Employee[]> GetDepartmentEmployeesFrom(int departmentId);


        //Department
        Task<Department> GetDepartment(int departmentId);
        Task<bool> existsDepartment(int departmentId);


    }
}
