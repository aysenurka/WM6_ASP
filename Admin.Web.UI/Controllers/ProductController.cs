using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using Admin.BLL.Helpers;
using Admin.BLL.Repository;
using Admin.BLL.Services;
using Admin.Models.Entities;
using Admin.Models.Models;
using Admin.Models.ViewModels;

namespace Admin.Web.UI.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            ViewBag.ProductList = GetProductSelectList();
            ViewBag.CategoryList = GetCategorySelectList();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(AddProductViewModel prd)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProductList = GetProductSelectList();
                ViewBag.CategoryList = GetCategorySelectList();
                return View(prd);
            }

            try
            {
                if (prd.Product.SupProductId.ToString().Replace("0", "").Replace("-", "").Length == 0)
                    prd.Product.SupProductId = null;

                prd.Product.LastPriceUpdateDate = DateTime.Now;
                Product model = new Product()
                {
                    Category = prd.Product.Category,
                    Description = prd.Product.Description,
                    ProductName = prd.Product.ProductName,
                    SalesPrice = prd.Product.SalesPrice,
                    BuyPrice = prd.Product.BuyPrice,
                    Id = prd.Product.Id,
                    AvatarPath = prd.Product.AvatarPath,
                    Barcode = prd.Product.Barcode,
                    CreatedDate = prd.Product.CreatedDate,
                    Invoices = prd.Product.Invoices,
                    LastPriceUpdateDate = prd.Product.LastPriceUpdateDate,
                    ProductType = prd.Product.ProductType,
                    Products = prd.Product.Products,
                    Quantity = prd.Product.Quantity,
                    SupProduct = prd.Product.SupProduct,
                    SupProductId = prd.Product.SupProductId,
                    UnitsInStock = prd.Product.UnitsInStock,
                    UpdatedDate = prd.Product.UpdatedDate
                };
                model.CategoryId = prd.Product.CategoryId;
                if (prd.PostedFile != null &&
                    prd.PostedFile.ContentLength > 0)
                {
                    var file = prd.PostedFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = StringHelpers.UrlFormatConverter(fileName);
                    fileName += StringHelpers.GetCode();
                    var klasoryolu = Server.MapPath("~/Upload/");
                    var dosyayolu = Server.MapPath("~/Upload/") + fileName + extName;

                    if (!Directory.Exists(klasoryolu))
                        Directory.CreateDirectory(klasoryolu);
                    file.SaveAs(dosyayolu);

                    WebImage img = new WebImage(dosyayolu);
                    img.Resize(250, 250, false);
                    img.AddTextWatermark("Wissen");
                    img.Save(dosyayolu);
                    model.AvatarPath = "/Upload/" + fileName + extName;
                }
                await new ProductRepo().InsertAsync(model);
                TempData["Message"] = $"{prd.Product.ProductName} isimli ürün başarıyla eklenmiştir";
                return RedirectToAction("Add");
            }
            catch (DbEntityValidationException ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {EntityHelpers.ValidationMessage(ex)}",
                    ActionName = "Add",
                    ControllerName = "Product",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {ex.Message}",
                    ActionName = "Add",
                    ControllerName = "Product",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult List()
        {
            try
            {
                List<Product> model = new ProductRepo().GetAll();
                if(model!=null)
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {ex.Message}",
                    ActionName = "List",
                    ControllerName = "Product",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("List", "Product");
        }
        
    }
}