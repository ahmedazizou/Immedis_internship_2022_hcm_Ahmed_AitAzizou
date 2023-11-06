using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IDepartmentService
    {
        int AddDepartment(Department department);
        void UpdateDepartment(Department department);
        Department GetDepartmentById(int Id);
        IQueryable<Department> GetAllDepartments();
        void DeleteDepartment(int Id);
    }
}
