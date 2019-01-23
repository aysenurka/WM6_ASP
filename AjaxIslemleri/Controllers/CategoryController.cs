using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxIslemleri.Models;
using AjaxIslemleri.Models.ViewModels;

namespace AjaxIslemleri.Controllers
{
    public class CategoryController : Controller
    {
        // get: category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Search(string s)
        {
            var key = s.ToLower();
            if (key.Length <= 2)
            {
                return Json(new ResponseData()
                {
                    message = "aramak için 2 karakterden fazla girilmelidir",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var db = new NorthwindEntities();
                db.Configuration.LazyLoadingEnabled = false;
                var data = db.Categories
                    .Where(x =>
                    x.CategoryName.ToLower().Contains(key) || x.Description.Contains(key))
                    .Select(x=>new CategoryViewModel
                    {
                        CategoryName = x.CategoryName,
                        Description = x.Description,
                        CategoryID = x.CategoryID,
                        ProductCount = x.Products.Count
                    })
                    .ToList();

                return Json(new ResponseData()
                {
                    message = $"{data.Count} adet kayit bulundu",
                    success = true,
                    data = data
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseData()
                {
                    message = $"bir hata olustu\n{ex.Message}",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }


            return null;
        }
    }
}