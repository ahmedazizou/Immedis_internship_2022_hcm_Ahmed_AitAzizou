using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HCMClient.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext db;
        public UserController(DataContext dataContext)
        {
            db = dataContext;
        }
        public IActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }
        public IActionResult Save(int Id = 0)
        {
            var user = new User();
            if (Id > 0)
            {
                user = db.Users.Find(Id);
            }
            var roles= new List<SelectListItem>();
            roles.Add(new SelectListItem()
            {
                Text = "Admin",
                Value = "Admin",
                Selected = user.Role == "Admin"
            });
            roles.Add(new SelectListItem() {
                Text = "HR",
                Value = "HR",
                Selected = user.Role == "HR"
            });
            ViewBag.roles = roles;
            return View(user);
        }
        [HttpPost]
        public IActionResult Save(User user)
        {
            if (ModelState.IsValid)
            {
                
                if (user.Id > 0)
                {
                    db.Users.Update(user);
                    db.SaveChanges();
                }
                else
                {user.Password=BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password);
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.roles = new List<SelectListItem>() { new SelectListItem {
            Text="Admin",
            Value="Admin",
            Selected=user.Role=="Admin"
            },
            new SelectListItem {
            Text="HR",
            Value="HR",
            Selected=user.Role=="HR"
            }};
            return View(user);
        }
        public IActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                var user = db.Users.Find(Id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
