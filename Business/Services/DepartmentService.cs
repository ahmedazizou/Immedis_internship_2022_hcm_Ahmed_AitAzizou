using Business.IServices;
using DataAccess.Data;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext dbContext;
        public DepartmentService(DataContext dataContext)
        {
            dbContext=dataContext;
        }
        public int AddDepartment(Department department)
        {
            var exist = dbContext.Departments.FirstOrDefault(x => x.Name == department.Name);
            if (exist!=null)
            {
                return -1;
            }
            dbContext.Add(department);
            dbContext.SaveChanges();
            return 0;
        }

        public void DeleteDepartment(int Id)
        {
            var exist = dbContext.Departments.Find(Id);
            if (exist!=null)
            {
                dbContext.Departments.Remove(exist); dbContext.SaveChanges();
            }
        }

        public IQueryable<Department> GetAllDepartments()
        {
            return dbContext.Departments.AsQueryable();
        }

        public Department GetDepartmentById(int Id)
        {
            return dbContext.Departments.Find(Id);
        }

        public void UpdateDepartment(Department department)
        {
            dbContext.Departments.Update(department);
            dbContext.SaveChanges();
        }
    }
}
