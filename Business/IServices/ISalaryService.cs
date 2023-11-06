using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IServices
{
    public interface ISalaryService
    {
        int AddSalary(Salary salary);
        void UpdateSalary(Salary salary);
        Salary GetSalaryById(int Id);
        IQueryable<Salary> GetAllSalaries();
        void DeleteSalary(int Id);
    }
}
