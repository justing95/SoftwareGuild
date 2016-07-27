using FlooringOrders.BLL.Responses;
using FlooringOrders.BLL.Tests.Mocks;
using FlooringOrders.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests
{
    [TestFixture]
    class ProductManagerTests
    {

        [Test]
        public void CanGetProduct()
        {
            IProductRepository repo = new MockGetProduct();
            ProductManager productManager = new ProductManager(repo);
            ProductResponse response = productManager.GetProduct("product");
            Product product = response.Product;
            Assert.IsNotNull(product);
        }

        [Test]
        public void GetProductFails()
        {
            IProductRepository repo = new MockInvalidProduct();
            ProductManager productManager = new ProductManager(repo);
            ProductResponse response = productManager.GetProduct("product");
            Assert.AreEqual(false, response.Success);
        }
        
    }
}
