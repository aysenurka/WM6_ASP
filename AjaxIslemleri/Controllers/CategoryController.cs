﻿using System;
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
            if (key.Length <= 2 && key != "*")
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
                List<CategoryViewModel> data;
                if (key == "*")
                {
                    data = db.Categories
                        .OrderBy(x => x.CategoryName)
                        .Select(x => new CategoryViewModel
                        {
                            CategoryName = x.CategoryName,
                            Description = x.Description,
                            CategoryID = x.CategoryID,
                            ProductCount = x.Products.Count
                        }).ToList();
                }
                else
                {
                    //db.Configuration.LazyLoadingEnabled = false;
                    data = db.Categories
                        .Where(x =>
                            x.CategoryName.ToLower().Contains(key) || x.Description.Contains(key))
                        .Select(x => new CategoryViewModel
                        {
                            CategoryName = x.CategoryName,
                            Description = x.Description,
                            CategoryID = x.CategoryID,
                            ProductCount = x.Products.Count
                        })
                        .ToList();
                }
               

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
        }

        [HttpPost]
        public ActionResult Add(CategoryViewModel model)
        {
            try
            {
                var db = new NorthwindEntities();
                db.Categories.Add(new Category()
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description
                });
                db.SaveChanges();
                return Json(new ResponseData()
                {
                    message = $"{model.CategoryName} ismindeki kategori başarıyla eklendi",
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseData()
                {
                    message = $"bir hata olustu {ex.Message}",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var db=new NorthwindEntities();
                var cat = db.Categories.Find(id);
                db.Categories.Remove(cat);
                db.SaveChanges();
                return Json(new ResponseData()
                {
                    message = $"{cat.CategoryName} ismindeki kategori başarıyla silindi",
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseData()
                {
                    message = $"bir hata olustu {ex.Message}",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}