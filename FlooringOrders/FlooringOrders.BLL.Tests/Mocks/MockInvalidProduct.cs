using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests.Mocks
{
    class MockInvalidProduct : IProductRepository
    {
        public Product GetProduct(string productType)
        {
            Product product = null;
            return product;
        }
    }
}
