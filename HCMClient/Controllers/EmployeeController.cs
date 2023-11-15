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
using HCMClient.Models;
using Newtonsoft.Json.Linq;

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
                return RedirectToAction("Login", "Account");
            }

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

            var employees = new List<Employee>();
            using (HttpClient client = new HttpClient(handler))
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
            ViewBag.departments = db.Departments.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString(), Selected = employee.DepartmentId == x.Id }).ToList();
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
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;


                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                    client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    var json = JsonSerializer.Serialize(employee);
                    var content = new StringContent(json, Encoding.UTF8, @"application/json");

                    var response = employee.Id > 0
                        ? await client.PostAsync("api/employee/EditEmployee", content)
                        : await client.PutAsync("api/employee", content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = $"Employee {(employee.Id > 0 ? "updated" : "created")} successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error occurred. Unable to save employee.";
                    }
                }
                return RedirectToAction("Index");
            }

            // If we get to this point, something failed, redisplay form
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
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;


                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                    client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                    var response = await client.DeleteAsync($"api/employee/{Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Employee deleted successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error occurred. Unable to delete employee.";
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int Id)
        {
            var employeeDetailsViewModel = new EmployeeDetailsViewModel
            {
                Employee = new Employee(),
                Salaries = new List<Salary>()
            };

            if (Id > 0)
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                    client.DefaultRequestHeaders.Authorization
                                 = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    // Fetch employee details
                    var employeeResponse = await client.GetAsync($"api/employee/{Id}");
                    if (employeeResponse.IsSuccessStatusCode)
                    {
                        var employeeResult = await employeeResponse.Content.ReadAsStringAsync();
                        var employeeData = JObject.Parse(employeeResult);

                        // Directly access the properties of the JSON object
                        var departmentName = employeeData["departmentName"]?.ToString();

                        // Now deserialize the Employee part of the JSON
                        employeeDetailsViewModel.Employee = employeeData.ToObject<Employee>();
                        employeeDetailsViewModel.Employee.Department = new Department { Name = departmentName };
                    }

                    // Fetch salaries for the employee
                    var salariesResponse = await client.GetAsync($"/api/Salaries/employee/{Id}");
                    if (salariesResponse.IsSuccessStatusCode)
                    {
                        var salariesResult = salariesResponse.Content.ReadAsStringAsync().Result;
                        employeeDetailsViewModel.Salaries = JsonSerializer.Deserialize<List<Salary>>(salariesResult);
                    }
                }
            }
            return View(employeeDetailsViewModel);
        }
    }
}
