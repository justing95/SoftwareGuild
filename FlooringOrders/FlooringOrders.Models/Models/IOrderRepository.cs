using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models
{
    public interface IOrderRepository
    {
        List<Order> GetOrders(DateTime date);
        void AddOrder(Order order);
        bool DeleteOrder(DateTime date, int orderNumber);
        Order EditOrder(DateTime date, int orderNumber);

    }
}
