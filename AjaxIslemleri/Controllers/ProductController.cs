using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxIslemleri.Models;
using AjaxIslemleri.Models.ViewModels;

namespace AjaxIslemleri.Controllers
{
    public class ProductController : Controller
    {
        // get: product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllCategories()
        {
            try
            {
                var db = new NorthwindEntities();
                var data = db.Categories.Select(x => new CategoryViewModel()
                {
                    CategoryName = x.CategoryName,
                    Description = x.Description,
                    CategoryID = x.CategoryID,
                    ProductCount = x.Products.Count
                }).ToList();
                return Json(new ResponseData()
                {
                    success = true,
                    data = data
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseData()
                {
                    success = false,
                    message = $"bir hata olustu\n{ex.Message}"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetAllProducts()
        {
            try
            {
                var db = new NorthwindEntities();
                var data = db.Products.OrderBy(x => x.ProductName)
                    .ToList()
                    .Select(x => new ProductViewModel()
                    {
                        CategoryName = x.Category?.CategoryName,
                        AddedDate = x.AddedDate,
                        CategoryID = x.CategoryID,
                        ProductName = x.ProductName,
                        ProductID = x.ProductID,
                        QuantityPerUnit = x.QuantityPerUnit,
                        UnitPrice = x.UnitPrice,
                        Discontinued = x.Discontinued,
                        UnitsInStock = x.UnitsInStock,
                        UnitsOnOrder = x.UnitsOnOrder,
                        SupplierID = x.SupplierID,
                        SupplierName = x.Supplier?.CompanyName,
                        ReorderLevel = x.ReorderLevel,
                        AddedDateFormatted = $"{x.AddedDate:g}",
                        UnitPriceFormatted = $"{x.UnitPrice:c2}"
                    })
                    .ToList();
                return Json(new ResponseData()
                {
                    success = true,
                    data = data
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseData()
                {
                    success = false,
                    message = $"bir hata olustu\n{ex.Message}"
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}