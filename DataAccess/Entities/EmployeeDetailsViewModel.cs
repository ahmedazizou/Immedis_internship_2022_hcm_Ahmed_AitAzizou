using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class EmployeeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public IEnumerable<Salary> Salaries { get; set; }
    }

}


