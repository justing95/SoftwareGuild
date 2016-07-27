using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests.Mocks
{
    class MockTestOrderNumber : IOrderRepository
    {
        List<Order> orders = new List<Order>();
        public void AddOrder(Order order)
        {
            Order newOrder = new Order(order.customerName, order.product, order.stateTax, order.area, order.date);
            Order newOrder2 = new Order(order.customerName, order.product, order.stateTax, order.area, order.date);
            orders.Add(newOrder);
            orders.Add(newOrder2);
        }

        public bool DeleteOrder(DateTime date, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public Order EditOrder(DateTime date, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders(DateTime date)
        {
            return orders;
        }
    }
}
