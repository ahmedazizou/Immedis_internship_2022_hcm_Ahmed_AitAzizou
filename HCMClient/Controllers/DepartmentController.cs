using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HCMClient.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly DataContext db;
        public DepartmentController(DataContext dataContext)
        {
            db = dataContext;
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
                try
                {
                    department.UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
                    if (department.Id > 0)
                    {
                        db.Departments.Update(department);
                        TempData["SuccessMessage"] = "Department updated successfully.";
                    }
                    else
                    {
                        db.Departments.Add(department);
                        TempData["SuccessMessage"] = "Department added successfully.";
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log the exception...
                    TempData["ErrorMessage"] = "Error saving department: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Validation failed. Please check the input fields.";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                var dept = db.Departments.Find(Id);
                if (dept != null)
                {
                    try
                    {
                        db.Departments.Remove(dept);
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Department deleted successfully.";
                    }
                    catch (Exception ex)
                    {
                        // Log the exception...
                        TempData["ErrorMessage"] = "Error deleting department: " + ex.Message;
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Department not found.";
                }
            }
            return RedirectToAction("Index");
        }
    }
}
