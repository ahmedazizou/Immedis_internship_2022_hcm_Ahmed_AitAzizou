using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
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

        // New fields added below

        [Required]
        [JsonPropertyName("month")]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        [JsonPropertyName("year")]
        [Range(1, 9999)]
        public int Year { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
        [JsonPropertyName("notes")]
        public string Notes { get; set; }
    }
}
