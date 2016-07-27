using HRPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HRPortal.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAll()
        {
            return Deserialize();
        }

        public Category Get(string categoryName, List<Category> categories)
        {
            return categories.FirstOrDefault(c => c.CategoryName.ToUpper() == categoryName.ToUpper());
        }

        public void Add(string categoryName)
        {
            var categories = Deserialize();
            int categoryID = categories.Max(c => c.CategoryID) + 1;
            Category category = new Category(categoryName, categoryID);
            categories.Add(category);
            Serialize(categories);
        }

        public void AddPolicy(Policy policy, string categoryName)
        {
            var categories = Deserialize();
            Category category = Get(categoryName, categories);
            if (category == null)
            {
                Add(categoryName);
                category = Get(categoryName, categories);
            }
            category.Policies.Add(policy);
            Serialize(categories);
        }

        public Category GetCategory(string categoryName)
        {
            var categories = Deserialize();
            var c = Get(categoryName, categories);
            return c;
        }

        public Policy GetPolicy(string policyName)
        {
            var categories = Deserialize();
            Policy p = null;
            foreach (var category in categories)
            {
                foreach (var policy in category.Policies)
                {
                    if (policyName == policy.PolicyTitle)
                    {
                        p = policy;
                    }
                }
            }
            return p;
        }

        public void DeletePolicy(Policy policy)
        {
            bool hasPolicy;
            var categories = Deserialize();
            foreach (var category in categories)
            {
                hasPolicy = false;
                foreach (var p in category.Policies)
                {
                    if (p.PolicyTitle.ToUpper() == policy.PolicyTitle.ToUpper())
                    {
                        hasPolicy = true;
                        policy = p;
                    }
                }
                if (hasPolicy)
                {
                    category.Policies.Remove(policy);
                    return;
                }
            }
            Serialize(categories);
        }

        public void DeleteCategory(string categoryName)
        {
            var categories = Deserialize();
            Category c = null;
            foreach (var category in categories)
            {
                if(categoryName.ToUpper() == category.CategoryName.ToUpper())
                {
                    c = category;
                }
            }
            categories.Remove(c);
            Serialize(categories);
        }

        public void Serialize(List<Category> categories)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string JsonString = serializer.Serialize(categories);
            File.WriteAllText(@"C:\Users\apprentice\Documents\repos\justin-gordon-individual-work\HRPortal\HRPortal\SerializedObjects\categories.txt", JsonString);
        }

        public List<Category> Deserialize()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string JsonString = File.ReadAllText(@"C:\Users\apprentice\Documents\repos\justin-gordon-individual-work\HRPortal\HRPortal\SerializedObjects\categories.txt");
            var categories = serializer.Deserialize<List<Category>>(JsonString);
            return categories;
        }
    }
}
