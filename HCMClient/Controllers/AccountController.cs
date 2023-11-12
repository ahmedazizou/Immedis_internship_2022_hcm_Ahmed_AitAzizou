using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using DataAccess.Entities;
using Services.IServices;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Web.Helpers;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace HCMClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string err = "")
        {
            ViewBag.Error = err;
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> PostLogin(User user)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("APIURL"));
                    var json = JsonSerializer.Serialize(user);
                    var content = new StringContent(json, Encoding.UTF8, @"application/json");
                    var response = await client.PostAsync("api/Login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var result1 = JObject.Parse(result);
                        var res = JsonSerializer.Deserialize<User>(result);
                        var id = result1["data"]["id"].ToString();
                        var role = result1["data"]["role"];
                        var token = result1["data"]["token"];

                        HttpContext.Session.SetInt32("UserId", Convert.ToInt16(id));
                        HttpContext.Session.SetString("Role", role.ToString());
                        HttpContext.Session.SetString("Token", token.ToString());
                        TempData["SuccessMessage"] = "Successfully logged in.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Assuming the API returns an error message in a known format
                        var result = await response.Content.ReadAsStringAsync();
                        var error = JObject.Parse(result)["message"].ToString();
                        TempData["ErrorMessage"] = error;
                    }
                }
            }
            // If model state is not valid, collect the errors and display them.
            var errors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));
            TempData["ErrorMessage"] = string.Join(" ", errors);
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
        // public IActionResult SignUp()
        // {
        //     User user = new User();
        //     return View();
        // }

    }
}
