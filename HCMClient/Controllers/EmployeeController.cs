using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace HCMClient.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext db;
        private readonly Microsoft.Extensions.Configuration.IConfiguration config;
        public EmployeeController(DataContext dataContext, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            db = dataContext;
            config = configuration;
            
        }
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Login","Account");
            }
            var employees=new List<Employee>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var response = await client.GetAsync("/api/Employee");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(result))
                    {
var res = JsonSerializer.Deserialize<List<Employee>>(result.ToString());
                    employees.AddRange(res);
                    }
                }
            }
                return View(employees);
        }
        public IActionResult Save(int Id = 0)
        {
            var employee = new Employee();
            if (Id > 0)
            {
                employee = db.Employees.Find(Id);
            }
            ViewBag.departments=db.Departments.Select(x=>new SelectListItem() { Text=x.Name,Value=x.Id.ToString(),Selected=employee.DepartmentId==x.Id}).ToList();
            return View(employee);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Employee employee)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Login", "Account");
            }
            if (ModelState.IsValid)
            {
                if (employee.Id > 0)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                        client.DefaultRequestHeaders.Authorization
                                     = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        var json = JsonSerializer.Serialize(employee);
                        var content = new StringContent(json, Encoding.UTF8, @"application/json");
                        var response = await client.PostAsync("api/employee/EditEmployee", content);
                    }
                        return RedirectToAction("Index");
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                        client.DefaultRequestHeaders.Authorization
                                     = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        var json = JsonSerializer.Serialize(employee);
                        var content = new StringContent(json, Encoding.UTF8, @"application/json");
                        var response = await client.PutAsync("api/employee", content);
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.departments = db.Departments.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString(), Selected = employee.DepartmentId == x.Id }).ToList();
            return View(employee);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Login", "Account");
            }
            if (Id > 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                    client.DefaultRequestHeaders.Authorization
                                 = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    var response = await client.DeleteAsync($"api/employee/{Id}");
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int Id)
        {
            var emp = new Employee();
            if (Id >0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                    client.DefaultRequestHeaders.Authorization
                                 = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    var response = await client.GetAsync($"api/employee/{Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        emp=JsonSerializer.Deserialize<Employee>(result);
                    }
                }
            }
            return View(emp);
        }
    }
}
