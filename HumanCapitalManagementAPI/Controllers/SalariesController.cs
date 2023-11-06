using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HumanCapitalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly DataContext db;
        public SalariesController(DataContext dataContext)
        {
                db = dataContext;
        }
        // GET: api/<SalariesController>
        [HttpGet]
        public IQueryable<Salary> Get()
        {
            return db.Salaries.AsQueryable();
        }

        // GET api/<SalariesController>/5
        [HttpGet("{id}")]
        public Salary Get(int id)
        {
            return db.Salaries.Find(id);
        }

        // POST api/<SalariesController>
        [HttpPut]
        public void Post([FromBody] Salary salary)
        {
            salary.TotalSalary = salary.BaseSalary + salary.Bonuses - salary.Deductions;
            db.Salaries.Add(salary);
            db.SaveChanges();   
        }

        // PUT api/<SalariesController>/5
        [HttpPost]
        public void EditSalary([FromBody] Salary value)
        {
            db.Salaries.Update(value);
            db.SaveChanges();
        }

        // DELETE api/<SalariesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
           var salary= db.Salaries.Find(id);
            if (salary!=null)
            {
                db.Salaries.Remove(salary);
                db.SaveChanges();
            }
        }
    }
}
