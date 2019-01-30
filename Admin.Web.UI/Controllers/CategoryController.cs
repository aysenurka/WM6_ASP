using System;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using Admin.BLL.Helpers;
using Admin.BLL.Repository;
using Admin.Models.Entities;
using Admin.Models.ViewModels;

namespace Admin.Web.UI.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            ViewBag.CategoryList = GetCategorySelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            try
            {
                if (model.SupCategoryId == 0)
                    model.SupCategoryId = null;
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("CategoryName", "Kategori adı 100 karakteri geçemez");
                    model.SupCategoryId = model.SupCategoryId ?? 0;
                    ViewBag.CategoryList = GetCategorySelectList();
                    return View(model);
                }

                if (model.SupCategoryId > 0)
                {
                    model.TaxRate = new CategoryRepo().GetById(model.SupCategoryId).TaxRate;
                }
                new CategoryRepo().Insert(model);
                TempData["Message"] = $"\n{model.CategoryName} isimli kategori başarıyla eklenmiştir";
                return RedirectToAction("Add");
            }
            catch (DbEntityValidationException ex)
            {
                // todo:hata sayfası yap
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu\n{EntityHelpers.ValidationMessage(ex)}",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                // todo:hata sayfası yap
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu\n{ex.Message}",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

    }
}