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
        public AccountController(IUserService userService,IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string err="")
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
                using (HttpClient client=new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("APIURL"));
                    //httpClient.DefaultRequestHeaders.Authorization
                    //             = new AuthenticationHeaderValue("Bearer", accessToken);
                    var json = JsonSerializer.Serialize(user);
                    var content = new StringContent(json, Encoding.UTF8, @"application/json");
                    var response =await client.PostAsync("api/Login", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync();
                        var result1 = JObject.Parse(result.Result);
                        var res = JsonSerializer.Deserialize<User>(result.Result);
                        var id = result1["data"]["id"].ToString();
                        var role = result1["data"]["role"];
                        var token = result1["data"]["token"];
                        HttpContext.Session.SetInt32("UserId", Convert.ToInt16(id));
                        HttpContext.Session.SetString("Role", role.ToString());
                        HttpContext.Session.SetString("Token",token.ToString());
                        return RedirectToAction("Index","Home");
                    }
                }
            }
            var errors = ModelState.Values.SelectMany(x => x.Errors).ToArray();
            return RedirectToAction("Login", "Account");
        }
        //public async Task<IActionResult> UserSignUp(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var exist = _dataContext.Users.FirstOrDefault(x => x.Email == user.Email);
        //            if (exist != null)
        //            {
        //                TempData["error"] = "This email already belongs to another user. Please try different email address.";
        //                return View("Login", user);
        //            }
        //            user.CreatedDate = DateTime.Now;
        //            user.IsAdmin = 2;
        //            _dataContext.Users.Add(user);
        //            _dataContext.SaveChanges();
        //            TempData["Succes"] = "Your account has been created successfully and pending for approval.";
        //            Utilitiy util = new Utilitiy(configuration);
        //            var adminEmail = configuration.GetValue<string>("AdminEmail") ?? string.Empty;
        //            var host = HttpContext.Request.Host;
        //            string Logo = "https://" + host + "/assets/images/logo.png";
        //            string LoginLink = "https://" + host + "/Account/CreatePassword?Id=" + user.Id;
        //            string body = EmailCE.ConfirmationEmail.Replace("{{Username}}", user.FirstName + " " + user.LastName).Replace("{{Logo}}", Logo);
        //            //.Replace("{{SuggestedBy}}", user.Email);
        //            await util.SendEmail("Registration", user.Email, body);
        //            string body1 = EmailCE.AdminNotification.Replace("{{Username}}", user.FirstName + " " + user.LastName)
        //                .Replace("{{Email}}", user.Email)
        //                .Replace("{{Phone}}", user.Phone)
        //                .Replace("{{Company}}", user.Comapny)
        //                .Replace("{{City}}", user.City)
        //                .Replace("{{Country}}", user.Country)
        //                .Replace("{{Logo}}", Logo);
        //            await util.SendEmail("New Customer Registration Request", adminEmail, body1);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["error"] = "Some Error Occured";
        //        }
        //    }
        //    var errors = ModelState.Values.SelectMany(x => x.Errors).ToArray();
        //    return View("Login", user);
        //}
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login");
        }
        public IActionResult SignUp()
        {
            User user = new User();
            return View();
        }
        //public IActionResult SignUp(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _dataContext.Add(user);
        //            _dataContext.SaveChanges();
        //            TempData["Succes"] = "Request Save Scussfully.";
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["error"] = "Some Error Occured";
        //        }
        //    }
        //    return View(user);
        //}
    }
}
