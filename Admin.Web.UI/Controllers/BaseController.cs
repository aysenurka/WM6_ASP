﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Admin.BLL.Repository;
using Admin.Models.Entities;

namespace Admin.Web.UI.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected List<SelectListItem> GetCategorySelectList()
        {
            var categories = new CategoryRepo().GetAll().OrderBy(x => x.CategoryName);
            var list = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Üst kategorisi yok",
                    Value = "0"
                }
            };
            foreach (var category in categories)
            {
                if (category.Categories.Any())
                {
                    list.AddRange(GetSubCategories(category.Categories.OrderBy(x => x.CategoryName).ToList()));
                }
                else
                {
                    list.Add(new SelectListItem()
                    {
                        Text = category.CategoryName,
                        Value = category.Id.ToString()
                    });
                }
            }

            List<SelectListItem> GetSubCategories(List<Category> categories2)
            {
                var list2 = new List<SelectListItem>();
                foreach (var category in categories2)
                {
                    if (category.Categories.Any())
                    {
                        list2.AddRange(GetSubCategories(category.Categories.ToList()));
                    }
                    else
                    {
                        list2.Add(new SelectListItem()
                        {
                            Text = category.CategoryName,
                            Value = category.Id.ToString()
                        });
                    }
                }

                return list2;
            }

            return list;
        }

        protected List<Product> GetProductList()
        {
            var products = new ProductRepo().GetAll()
                .OrderBy(x => x.ProductName)
                .ToList();

            return products;
        }
    }
}