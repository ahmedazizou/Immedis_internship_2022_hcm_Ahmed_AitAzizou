using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HumanCapitalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext db;
        public EmployeeController(DataContext dataContext)
        {
            db = dataContext;
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        public IQueryable<Employee> Get()
        {
            return db.Employees.AsQueryable();
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        // POST api/<EmployeeController>
        [HttpPut]
        public void Post([FromBody] Employee value)
        {
            db.Employees.Add(value);
            db.SaveChanges();
        }

        // PUT api/<EmployeeController>/5
        [HttpPost]
        public void EditEmployee([FromBody] Employee emp)
        {
            db.Employees.Update(emp);
            db.SaveChanges();
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var emp=db.Employees.Find(id);
            if (emp!=null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
            }
        }
    }
}
