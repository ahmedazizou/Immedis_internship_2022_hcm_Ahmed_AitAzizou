using DataAccess.Data;
using DataAccess.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<EmployeeDto>> Get()
        {
            var employees = db.Employees
                              .Include(e => e.Department)
                              .Select(e => new EmployeeDto
                              {
                                  Id = e.Id,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  Email = e.Email,
                                  DOB = e.DOB.ToString("yyyy-MM-dd"), // Assuming you want a string representation
                                  DateOfJoin = e.DateOfJoin.ToString("yyyy-MM-dd"),
                                  DepartmentId = e.DepartmentId,
                                  DepartmentName = e.Department.Name
                              })
                              .ToList();

            return Ok(employees);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public ActionResult<EmployeeDto> Get(int id)
        {
            var employee = db.Employees
                             .Include(e => e.Department)
                             .Where(e => e.Id == id)
                             .Select(e => new EmployeeDto
                             {
                                 Id = e.Id,
                                 FirstName = e.FirstName,
                                 LastName = e.LastName,
                                 Email = e.Email,
                                 DOB = e.DOB.ToString("yyyy-MM-dd"), // Format the date as desired
                                 DateOfJoin = e.DateOfJoin.ToString("yyyy-MM-dd"),
                                 DepartmentId = e.DepartmentId,
                                 DepartmentName = e.Department.Name
                             })
                             .FirstOrDefault();

            if (employee == null)
            {
                return NotFound(new { message = "Employee not found." });
            }

            return Ok(employee);
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
            var emp = db.Employees.Find(id);
            if (emp != null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
            }
        }
    }
}
