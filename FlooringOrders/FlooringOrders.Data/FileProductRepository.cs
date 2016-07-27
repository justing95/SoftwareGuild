using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class FileProductRepository : IProductRepository
    {
        List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            bool exists = File.Exists(GetDataFilePath());
            if (exists)
            {
                string[] splitProduct;
                string productString;
                using (var stream = File.OpenRead(GetDataFilePath()))
                using (var reader = new StreamReader(stream))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        productString = reader.ReadLine();
                        splitProduct = productString.Split(',');
                        string productType = splitProduct[0];
                        decimal costPerFoot;
                        decimal.TryParse(splitProduct[1], out costPerFoot);
                        decimal laborPerFoot;
                        decimal.TryParse(splitProduct[2], out laborPerFoot);
                        Product product = new Product(productType, costPerFoot, laborPerFoot);
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        private string GetDataFilePath()
        {
            return (@"C:\Users\apprentice\Desktop\repos\justin-gordon-individual-work\FlooringOrders\ProductsAndTaxes\Products.txt");
        }

        public Product GetProduct(string productType)
        {
            List<Product> products = GetProducts();
            Product product = null;
            foreach(Product p in products)
            {
                if (p.productType.ToUpper() == productType.ToUpper())
                {
                    product = p;
                }
            }
            return product;
        }
    }
}
