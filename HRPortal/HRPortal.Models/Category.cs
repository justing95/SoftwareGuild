using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models
{
    public class Category
    {
        [Required(ErrorMessage = "Please input a category name")]
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public List<Policy> Policies { get; set; }

        public Category()
        {
            Policies = new List<Policy>();
        }

        public Category(string categoryName, int categoryID)
        {
            CategoryName = categoryName;
            CategoryID = categoryID;
            Policies = new List<Policy>();
        }
    }
}
