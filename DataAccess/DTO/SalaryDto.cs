using System.ComponentModel.DataAnnotations;

namespace DataAccess.DTO
{
    public class SalaryDto
    {
        public decimal BaseSalary { get; set; }
        public decimal Bonuses { get; set; }
        public decimal Deductions { get; set; }
        [Range(1, 12, ErrorMessage = "Invalid month. Must be between 1 and 12.")]
        public int Month { get; set; }
        [Range(2000, 2100, ErrorMessage = "Invalid year. Must be between 2000 and 2100.")]
        public int Year { get; set; }
        public int EmployeeId { get; set; }
        public string Notes { get; set; }
    }
}
