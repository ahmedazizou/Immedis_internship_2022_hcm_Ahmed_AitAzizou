using DataAccess.Data;
using DataAccess.DTO;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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


        // GET api/<SalariesController>/employee/5
        [HttpGet("employee/{employeeId}")]
        public IActionResult GetSalariesForEmployee(int employeeId)
        {
            var salaries = db.Salaries.Where(s => s.EmployeeId == employeeId).ToList();

            if (!salaries.Any())
            {
                return NotFound(new { message = $"Salaries for employee with ID {employeeId} not found." });
            }

            return Ok(salaries);
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
        [HttpPost]
        public IActionResult Post([FromBody] SalaryDto salaryDto)
        {
            var existingSalary = db.Salaries
                .FirstOrDefault(s => s.EmployeeId == salaryDto.EmployeeId
                                     && s.Month == salaryDto.Month
                                     && s.Year == salaryDto.Year);

            if (existingSalary != null)
            {
                return Conflict(new { message = "Salary record for this employee, month, and year already exists." });
            }

            var salary = new Salary
            {
                BaseSalary = salaryDto.BaseSalary,
                Bonuses = salaryDto.Bonuses,
                Deductions = salaryDto.Deductions,
                TotalSalary = salaryDto.BaseSalary + salaryDto.Bonuses - salaryDto.Deductions,
                Month = salaryDto.Month,
                Year = salaryDto.Year,
                EmployeeId = salaryDto.EmployeeId,
                Notes = salaryDto.Notes,
                CreatedAt = DateTime.UtcNow
            };

            db.Salaries.Add(salary);
            db.SaveChanges();

            // 
            return CreatedAtAction(nameof(Get), new { id = salary.Id }, salary);
        }


        // PUT api/<SalariesController>/5
        [HttpPut("{id}")]
        public IActionResult EditSalary(int id, [FromBody] SalaryDto salaryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salaryToUpdate = db.Salaries.Find(id);
            if (salaryToUpdate == null)
            {
                return NotFound();
            }

            // Manually map the DTO to the existing entity
            salaryToUpdate.BaseSalary = salaryDto.BaseSalary;
            salaryToUpdate.Bonuses = salaryDto.Bonuses;
            salaryToUpdate.Deductions = salaryDto.Deductions;
            salaryToUpdate.TotalSalary = salaryDto.BaseSalary + salaryDto.Bonuses - salaryDto.Deductions;
            salaryToUpdate.Month = salaryDto.Month;
            salaryToUpdate.Year = salaryDto.Year;
            salaryToUpdate.Notes = salaryDto.Notes;
            salaryToUpdate.UpdatedAt = DateTime.UtcNow;

            db.SaveChanges();
            return Ok(salaryToUpdate);
        }

        // DELETE api/<SalariesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return NotFound();
            }

            db.Salaries.Remove(salary);
            db.SaveChanges();
            return Ok(new { message = "Salary deleted successfully." });
        }

    }
}
