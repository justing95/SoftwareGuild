using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class MemoryProductRepository : IProductRepository
    {
        List<Product> products = new List<Product>();
        public MemoryProductRepository()
        {
            Product product1 = new Product("Brick", 14.5m, 12.2m);
            Product product2 = new Product("Tile", 5.0m, 8.0m);
            Product product3 = new Product("Ceramic", 10.1m, 10.4m);
            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
        }
        public Product GetProduct(string productType)
        {
            Product product = null;
            foreach (Product p in products)
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
