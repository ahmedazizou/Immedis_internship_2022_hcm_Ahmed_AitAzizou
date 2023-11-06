using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Entities
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        [Required]
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; set; } = DateTime.Now;
        [NotMapped]
        public string token { get; set; }
    }
}
