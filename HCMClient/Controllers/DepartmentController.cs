using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HCMClient.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DataContext db;
        public DepartmentController(DataContext dataContext)
        {
            db=dataContext;
        }
        public IActionResult Index()
        {
            var depts = db.Departments.ToList();
            return View(depts);
        }
        public IActionResult Save(int Id = 0)
        {
            var dept = new Department();
            if (Id > 0)
            {
                dept = db.Departments.Find(Id);
            }
            return View(dept);
        }
        [HttpPost]
        public IActionResult Save(Department department)
        {
            if (ModelState.IsValid)
            {
                department.UserId = HttpContext.Session.GetInt32("UserId")??0;
                if (department.Id > 0)
                {
                    db.Departments.Update(department);
                    db.SaveChanges();
                }
                else
                {
                    db.Departments.Add(department);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                var user = db.Departments.Find(Id);
                if (user != null)
                {
                    db.Departments.Remove(user);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
