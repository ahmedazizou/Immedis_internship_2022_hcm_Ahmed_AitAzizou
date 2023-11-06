using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface IEmployeeService
    {
        int AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        Employee GetEmployeeById(int Id);
        IQueryable<Employee> GetAllEmployees();
        void DeleteEmployee(int Id);
    }
}
