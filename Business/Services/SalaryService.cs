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
    public class SalaryService : ISalaryService
    {
        private readonly DataContext dbContext;
        public SalaryService(DataContext dataContext)
        {
            dbContext= dataContext;
        }
        public int AddSalary(Salary salary)
        {
            dbContext.Salaries.Add(salary);
            dbContext.SaveChanges();
            return 0;
        }

        public void DeleteSalary(int Id)
        {
            var exist = dbContext.Salaries.Find(Id);
            if (exist != null)
            {
                dbContext.Salaries.Remove(exist); dbContext.SaveChanges();
            }
        }

        public IQueryable<Salary> GetAllSalaries()
        {
            return dbContext.Salaries.AsQueryable();
        }

        public Salary GetSalaryById(int Id)
        {
            return dbContext.Salaries.Find(Id);
        }

        public void UpdateSalary(Salary salary)
        {
            dbContext.Salaries.Update(salary);
            dbContext.SaveChanges();
        }
    }
}
