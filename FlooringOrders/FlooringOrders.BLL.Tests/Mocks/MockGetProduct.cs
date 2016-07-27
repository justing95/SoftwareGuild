using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests.Mocks
{
    class MockGetProduct : IProductRepository
    {
        public Product GetProduct(string productType)
        {
            Product product = new Product("Tile", 15.0m, 20.0m);
            return product;
        }
    }
}
