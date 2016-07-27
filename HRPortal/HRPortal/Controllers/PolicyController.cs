using HRPortal.Data;
using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRPortal.Controllers
{
    public class PolicyController : Controller
    {
        
        CategoryRepository CategoryRepo = new CategoryRepository();
        // GET: Policy
        [HttpGet]
        public ActionResult ViewPolicies()
        {
            var model = CategoryRepo.GetAll();
            return View(model);
        }

        public ActionResult ManagePolicies()
        {
            var model = CategoryRepo.GetAll();
            return View(model);
        }

        [HttpGet]
        public ActionResult AddPolicy()
        {
            var VM = new AddPolicyViewModel();
            VM.SetCategoryItems(CategoryRepo.GetAll());
            return View(VM);
        }

        [HttpPost]
        public ActionResult AddPolicy(AddPolicyViewModel VM)
        {
            CategoryRepo.AddPolicy(VM.Policy, VM.Policy.CategoryName);
            return RedirectToAction("ManagePolicies");
        }
        
        [HttpGet]
        public ActionResult DeletePolicy(string policyName)
        {
            Policy policy = CategoryRepo.GetPolicy(policyName);
            return View(policy);
        }

        [HttpPost]
        public ActionResult DeletePolicy(Policy policy)
        {
            CategoryRepo.DeletePolicy(policy);
            return RedirectToAction("ManagePolicies");

        }
    }
}