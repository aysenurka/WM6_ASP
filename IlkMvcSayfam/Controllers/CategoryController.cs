using IlkMvcSayfam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlkMvcSayfam.Controllers
{
    public class CategoryController:Controller
    {
        // get: category
        public ActionResult Index() {
            var data = new NorthwindSabahEntities()
                .Categories
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(data);
        }

        public ActionResult Detail(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            var data = new NorthwindSabahEntities().Categories.Find(id.Value);
            if (data == null) RedirectToAction("Index");
            return View(data);
        }
    }
}