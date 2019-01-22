using IlkMvcSayfam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlkMvcSayfam.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            var data = new NorthwindSabahEntities()
                .Employees
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToList();
            return View(data);
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var data = new NorthwindSabahEntities()
                .Employees
                .Find(id.Value);

            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }

        public ActionResult Sil(int? id)
        {
            var db = new NorthwindSabahEntities();
            try
            {
                var employee = db
                         .Employees
                         .Find(id.GetValueOrDefault());
                if (employee == null)
                    return RedirectToAction("Index", "Employee");

                db.Employees.Remove(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Ekle(Employee employee)
        {
            try
            {
                var db = new NorthwindSabahEntities();
                if (employee.HireDate == null)
                    employee.HireDate = DateTime.Now;
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            var db = new NorthwindSabahEntities();
            try
            {
                var employee = db
                    .Employees
                    .Find(id.GetValueOrDefault());

                if (employee == null)
                    return RedirectToAction("Index", "Employee");

                return View(employee);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Employee");
            }
        } 


        [HttpPost]
        public ActionResult Update(Employee employee)
        {
            try
            {
                var db = new NorthwindSabahEntities();
                var guncel = db
                    .Employees
                    .Find(employee.EmployeeID);

                if (guncel == null)
                    return RedirectToAction("Index");

                guncel.FirstName = employee.FirstName;
                guncel.LastName = employee.LastName;
                guncel.HomePhone = employee.HomePhone;
                guncel.BirthDate = employee.BirthDate;
                guncel.Address = employee.Address;
                guncel.HireDate = employee.HireDate;

                db.SaveChanges();
                ViewBag.Message = "<span class='text text-success'>Update Successfully</span>";
                return View(guncel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"<span class='text text-danger'>Update Error {ex.Message}</span>";
                return View(employee);
            }
        }
    }
}