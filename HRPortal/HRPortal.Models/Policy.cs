using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models
{
    public class Policy
    {
        [Required (ErrorMessage = "You didn't give your policy any content")]
        public string PolicyContent { get; set; }
        [Required (ErrorMessage = "Please input a policy name")]
        public string PolicyTitle { get; set; }
        [Required (ErrorMessage = "Make sure to include a category")]
        public string CategoryName { get; set; }
        public int PolicyID { get; set; }

        public Policy()
        {

        }

        public Policy(string content, string title, int id)
        {
            PolicyContent = content;
            PolicyTitle = title;
            PolicyID = id;
        }
    }

}
