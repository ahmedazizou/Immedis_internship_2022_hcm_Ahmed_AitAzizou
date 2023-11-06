using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using AutoMapper;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace HCMClient.Controllers
{
    public class SalaryController : Controller
    {
        private readonly DataContext db;
        private readonly Microsoft.Extensions.Configuration.IConfiguration config;
        public SalaryController(DataContext dataContext,Microsoft.Extensions.Configuration.IConfiguration config)
        {
            db=dataContext;
            this.config=config;
        }
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Login", "Account");
            }
            var salaries = new List<Salary>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                client.DefaultRequestHeaders.Authorization
                             = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                var response = await client.GetAsync("/api/Salaries");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(result))
                    {
                        var res = JsonSerializer.Deserialize<List<Salary>>(result.ToString());
                        salaries.AddRange(res);
                    }
                }
            }
            return View(salaries);
        }
        public IActionResult Save(int Id = 0)
        {
            var salary = new Salary();
            if (Id > 0)
            {
                salary = db.Salaries.Find(Id);
            }
            ViewBag.employees = db.Employees.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.Id.ToString(), Selected = x.Id == salary.EmployeeId }).ToList();

            return View(salary);
        }
        [HttpPost]
        public async Task<IActionResult> Save(Salary salary)
        {
            if (ModelState.IsValid)
            {
                if (salary.Id > 0)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                        client.DefaultRequestHeaders.Authorization
                                     = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        var json = JsonSerializer.Serialize(salary);
                        var content = new StringContent(json, Encoding.UTF8, @"application/json");
                        var response = await client.PostAsync("api/Salaries/EditSalary", content);
                    }
                }
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                        client.DefaultRequestHeaders.Authorization
                                     = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        var json = JsonSerializer.Serialize(salary);
                        var content = new StringContent(json, Encoding.UTF8, @"application/json");
                        var response = await client.PutAsync("api/Salaries", content);
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.employees = db.Employees.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.Id.ToString(), Selected = x.Id == salary.EmployeeId }).ToList();
            return View(salary);
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
                    var response = await client.DeleteAsync($"api/Salaries/{Id}");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
