using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace HumanCapitalManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContext db;
        public LoginController(IConfiguration config, DataContext dataContext)
        {
            _config = config; db = dataContext;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
            User user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                user.token = tokenString;
                var result = new User()
                {
                    Id = user.Id,
                    Role = user.Role,
                    Username = user.Username,
                    Password = user.Password,
                    Created = user.Created,
                    token = tokenString
                };
                response = Ok(new { data = result });
            }
            return response;
        }
        User AuthenticateUser(User loginCredentials)
        {
            User user = db.Users.FirstOrDefault(x => x.Username == loginCredentials.Username);
            if (user != null)
            {
                bool temp = BCrypt.Net.BCrypt.EnhancedVerify(loginCredentials.Password, user.Password);
                if (temp)
                {
                    return user;
                }
            }
            return null;
        }
        public string GenerateJWTToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:SecretKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
            new Claim("fullName", userInfo.Username.ToString()),
            new Claim("role",userInfo.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config.GetSection("Jwt:Issuer").Value,
            audience: _config.GetSection("Jwt:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}