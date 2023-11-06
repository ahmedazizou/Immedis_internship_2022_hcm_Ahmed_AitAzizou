using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class Salary
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        [JsonPropertyName("baseSalary")]
        public decimal BaseSalary { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        [JsonPropertyName("bonuses")]
        public decimal Bonuses { get; set; }

        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        [JsonPropertyName("deductions")]
        public decimal Deductions { get; set; }

        [JsonPropertyName("totalSalary")]
        public decimal TotalSalary { get; set; }

        [Required]
        [JsonPropertyName("employeeId")]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
