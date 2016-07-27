using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class MemoryOrderRepository : IOrderRepository
    {
        List<Order> orders = new List<Order>();
        public MemoryOrderRepository()
        {
            Product product = new Product("Brick", 0.5m, 1m);
            StateTax tax = new StateTax("MN", 6.0m);
            Order order1 = new Order("Gary", product, tax, 100m, new DateTime(6 / 12 / 2012));
            Order order2 = new Order("Joseph", product, tax, 50m, new DateTime(6 / 12 / 2012));
            Order order3 = new Order("Lucas", product, tax, 75m, new DateTime(6 / 12 / 2012));
            orders.Add(order1);
            orders.Add(order2);
            orders.Add(order3);
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }

        public bool DeleteOrder(DateTime date, int orderNumber)
        {
            Order o = null;
            foreach (Order order in orders)
            {
                if (order.date == date && order.orderNumber == orderNumber)
                {
                    o = order;
                }
            }
            if (o == null)
            {
                return false;
            }
            orders.Remove(o);
            return true;
        }

        public Order EditOrder(DateTime date, int orderNumber)
        {
            Order o = null;
            foreach (Order order in orders)
            {
                if (order.date == date && order.orderNumber == orderNumber)
                {
                    o = order;
                }
            }
            return o;
        }

        public List<Order> GetOrders(DateTime date)
        {
            return orders;
        }
    }
}
