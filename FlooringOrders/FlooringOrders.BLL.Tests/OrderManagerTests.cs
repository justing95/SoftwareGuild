using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringOrders.BLL;
using FlooringOrders.Models;
using FlooringOrders.BLL.Tests.Mocks;
using FlooringOrders.BLL.Responses;

namespace FlooringOrders.BLL.Tests
{
    [TestFixture]
    class OrderManagerTests
    {
        
        [Test]
        public void CanGetOrder()
        {
            IOrderRepository repo = new MockGiveOrder();
            DateTime date = new DateTime(2016, 6, 15);
            OrderManager orderManager = new OrderManager(repo);

            List<Order> orders = orderManager.GetOrders(date);
            Assert.IsNotNull(orders);
        }
        [Test]
        public void CanAddOrder()
        {
            IOrderRepository repo = new MockAddOrder();
            DateTime date = new DateTime(2016, 6, 15);
            OrderManager orderManager = new OrderManager(repo);
            Order order = new Order();
            List<Order> orders = orderManager.GetOrders(date);
            orderManager.AddOrder(order);
            Assert.AreEqual(order, orders.Last());
        }
        [Test]
        public void CanDeleteOrder()
        {
            IOrderRepository repo = new MockAddOrder();
            DateTime date = new DateTime(2016, 6, 15);
            OrderManager orderManager = new OrderManager(repo);
            Order order = new Order();
            List<Order> orders = orderManager.GetOrders(date);
            orderManager.AddOrder(order);
            orderManager.DeleteOrder(date, 2);
            Assert.AreEqual(orders[1], order);

        }

        [Test]
        public void TestOrderNumber()
        {
            IOrderRepository repo = new MockTestOrderNumber();
            IProductRepository productRepo = new MockGetProduct();
            IStateTaxRepository taxRepo = new MockGetTaxes();
            OrderManager orderManager = new OrderManager(repo);
            ProductManager productManager = new ProductManager(productRepo);
            TaxManager taxManager = new TaxManager(taxRepo);
            TaxResponse response = taxManager.GetTaxes("MN");
            StateTax tax = response.StateTax;
            ProductResponse productResponse = productManager.GetProduct("Tile");
            Product product = productResponse.Product;
            Order order = new Order("John", product, tax, 100.0m, DateTime.Now);
            orderManager.AddOrder(order);
            List<Order> orders = orderManager.GetOrders(DateTime.Now);
            Assert.AreEqual(orders[0].orderNumber + 1, orders[1].orderNumber);
        }

        [Test]
        public void InvalidDeleteFails()
        {
            IOrderRepository repo = new MockInvalidOrder();
            OrderManager orderManager = new OrderManager(repo);
            OrderResponse response = orderManager.DeleteOrder(DateTime.Now, 14);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void ValidDeleteSucceeds()
        {
            IOrderRepository repo = new MockValidOrder();
            OrderManager orderManager = new OrderManager(repo);
            OrderResponse response = orderManager.DeleteOrder(DateTime.Now, 14);
            Assert.IsTrue(response.Success);
        }
    }
}
