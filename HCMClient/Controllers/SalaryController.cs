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
        public SalaryController(DataContext dataContext, Microsoft.Extensions.Configuration.IConfiguration config)
        {
            db = dataContext;
            this.config = config;
        }
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return RedirectToAction("Login", "Account");
            }

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;


            var salaries = new List<Salary>();

            using (HttpClient client = new HttpClient(handler))
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
        public async Task<IActionResult> Save(Salary salary, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {

                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (HttpClient client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(config.GetValue<string>("APIURL"));
                    client.DefaultRequestHeaders.Authorization
                                 = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    var json = JsonSerializer.Serialize(salary);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage response;

                    // Check if we're adding or updating
                    if (salary.Id > 0)
                    {
                        // Update existing salary (PUT)
                        response = await client.PutAsync($"api/Salaries/{salary.Id}", content);
                    }
                    else
                    {
                        // Create new salary (POST)
                        response = await client.PostAsync("api/Salaries", content);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Salary saved successfully.";
                        // If returnUrl is provided and valid, redirect back to it
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            // Otherwise, redirect to the index page of salaries
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        // Failure
                        TempData["ErrorMessage"] = "Error occurred. Status code: " + response.StatusCode;
                    }
                }
            }

            // If we get to this point, something went wrong
            ViewBag.employees = db.Employees.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id.ToString(),
                Selected = x.Id == salary.EmployeeId
            }).ToList();
            TempData["ErrorMessage"] = "Please correct the errors on the form.";
            return View(salary);
        }

        public IActionResult AddSalaryForEmployee(int employeeId)
        {
            var salary = new Salary
            {
                EmployeeId = employeeId
            };

            // Set the returnUrl for the view to redirect after saving
            ViewData["ReturnUrl"] = Url.Action("Details", "Employee", new { id = employeeId });

            return View("AddSalaryForEmployee", salary);
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
                    var response = await client.DeleteAsync($"api/Salaries/{Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Salary deleted successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error occurred. Status code: " + response.StatusCode;
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
