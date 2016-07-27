using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests
{
    class MockGiveOrder : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
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
            List<Order> orders = new List<Order>();
            if (date == new DateTime(2016, 6, 15))
            {
                Order order1 = new Order();
                Order order2 = new Order();
                orders.Add(order1);
                orders.Add(order2);
            }
            return orders;
        }
        
        
    }
}
