using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<Employee> Employees { get; set; }
    }
}
