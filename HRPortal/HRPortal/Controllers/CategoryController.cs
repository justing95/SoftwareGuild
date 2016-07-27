using HRPortal.Data;
using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository repo = new CategoryRepository();
        // GET: Category
        public ActionResult ManageCategories()
        {
            List<Category> model = repo.GetAll();
            return View(model);
        }
        
       [HttpGet]
       public ActionResult DeleteCategory(string categoryName)
        {
            var category = repo.GetCategory(categoryName);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(Category category)
        {
            repo.DeleteCategory(category.CategoryName);
            return RedirectToAction("ManageCategories");
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            repo.Add(category.CategoryName);
            return RedirectToAction("ManageCategories");
        }


    }
}