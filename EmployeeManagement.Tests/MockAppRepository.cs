using Employeemanagement.DatabaseBuild;
using Employeemanagement.DatabaseBuild.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests
{
    public class MockAppRepository : TestWithSqllite , IAppRepository
    {
        //private readonly AppDbContext contxt;

        public MockAppRepository()
        {
            //contxt = context;
            var departmenta = new Department() 
            {
                Id = 234,
                DepartmentName = "Tajnya" 
            };
            var employee1 = new Employee()
            {
                Id = 1,
                Name = "Dhananjay",
                SurName = "Veer",
                Address = "Behind Smaarak,Raj Niwaas ",
                Department = departmenta,
                PhoneNumber = "3423423454",
                Qualification = "Visheshjajnya"
            };
            var employee2 = new Employee()
            {
                Id = 2,
                Name = "Tanmay",
                SurName = "Vyom",
                Address = "Behind Smaarak,Raj Niwaas ",
                Department = departmenta,
                PhoneNumber = "3423423454",
                Qualification = "Visheshjajnya"
            };

            context.Departments.Add(departmenta);
            context.SaveChanges();
            context.Employees.Add(employee1);
            context.Employees.Add(employee2);
            context.SaveChanges();
        }


        //General
        public void Add<T>(T entity) where T : class
        {
            context.Add<T>(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove<T>(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ((await context.SaveChangesAsync()) > 0);
        }

        //Department
        public async Task<bool> existsDepartment(int departmentId)
        {
            return await context.Departments
                                .Where(dp => dp.Id == departmentId)
                                .AnyAsync();
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await context.Departments
                            .Where(dp => dp.Id == departmentId)
                            .FirstAsync();
        }

        //Employee
        public async Task<bool> existsEmployee(int empid)
        {
            return await context.Employees
                                 .Where(emp => emp.Id == empid)
                                 .AnyAsync();
        }

         async Task<Employee> IAppRepository.GetEmployee(int empid)
        {

            return await context.Employees.Include(emp => emp.Department)
                                .Where(emp => emp.Id == empid)
                                .FirstAsync();
        }

        public async Task<Employee[]> GetDepartmentEmployeesFrom(int departmentId)
        {
            return await context.Employees
                                .Where(emp => emp.Department.Id == departmentId)
                                .ToArrayAsync();
        }
    }
}
