using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRPortal.Models
{
    public class AddPolicyViewModel
    {
        public Policy Policy { get; set; }
        public List<SelectListItem> CategoryItems { get; set; }

        public AddPolicyViewModel()
        {
            Policy = new Policy();
            CategoryItems = new List<SelectListItem>();
        }

        public void SetCategoryItems(List<Category> categories)
        {
            foreach (var category in categories)
            {
                CategoryItems.Add(new SelectListItem
                {
                    Text = category.CategoryName,
                    Value = category.CategoryName
                });
            }
        }
    }
}
