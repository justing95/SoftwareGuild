using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Models
{
    public interface ICategoryRepository
    {
       // List<Category> categories { get; set; }
        List<Category> GetAll();
        Category Get(string categoryName, List<Category> categories);
        void Add(string categoryName);

    }
}
