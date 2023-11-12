using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccess.Entities
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [Required]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [Required]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please choose date of birth")]
        [Display(Name = "Date of Birth")]
        [JsonPropertyName("dob")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [JsonPropertyName("dateOfJoin")]
        [Required(ErrorMessage = "Please choose date of join")]
        [Display(Name = "Date of Join")]
        [DataType(DataType.Date)]
        public DateTime DateOfJoin { get; set; }

        [Required]
        [JsonPropertyName("departmentId")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

    }
}
