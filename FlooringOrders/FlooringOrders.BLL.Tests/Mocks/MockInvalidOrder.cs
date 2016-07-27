using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests.Mocks
{
    class MockInvalidOrder : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public bool DeleteOrder(DateTime date, int orderNumber)
        {
            return false;
        }

        public Order EditOrder(DateTime date, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrders(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
