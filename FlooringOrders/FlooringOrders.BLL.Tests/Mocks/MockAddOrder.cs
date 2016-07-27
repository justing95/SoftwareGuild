using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests.Mocks
{
    class MockAddOrder : IOrderRepository
    {
        List<Order> orders;
        public List<Order> GetOrders(DateTime date)
        {
            orders = new List<Order>();
            if (date == new DateTime(2016, 6, 15))
            {
                Order order1 = new Order()
                {
                    orderNumber = 1,
                    date = new DateTime(2016, 6, 15)
                };
                Order order2 = new Order()
                {
                    orderNumber = 2,
                    date = date
                };
                orders.Add(order1);
                orders.Add(order2);
            }
            return orders;
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public void EditOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(DateTime date, int orderNumber)
        {
            Order order = null;
            foreach (Order o in orders)
            {
                if (o.date == date && o.orderNumber == orderNumber)
                {
                    order = o;
                }
            }
            orders.Remove(order);
            return true;
        }

        public Order EditOrder(DateTime date, int orderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
