using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlooringOrders.Models;

namespace FlooringOrders.Data
{
    public class FileOrderRepository : IOrderRepository
    {
        public void AddOrder(Order order)
        {
            List<Order> orders = GetOrders(order.date);
            orders.Add(order);
            WriteToFile(orders, order.date);
        }

        public bool DeleteOrder(DateTime date, int orderNumber)
        {
            List<Order> orders = GetOrders(date);
            Order delete = null;
            foreach (Order order in orders)
            {
                if (orderNumber == order.orderNumber)
                {
                    delete = order;
                }
            }
            if (delete == null)
            {
                return false;
            }
            orders.Remove(delete);
            WriteToFile(orders, date);
            return true;
        }

        public Order EditOrder(DateTime date, int orderNumber)
        {
            List<Order> orders = GetOrders(date);
            Order edit = null;
            foreach (Order order in orders)
            {
                if (orderNumber == order.orderNumber)
                {
                    edit = order;
                }
            }
            return edit;
        }

        public List<Order> GetOrders(DateTime date)
        {
            List<Order> orders = new List<Order>();
            bool exists = File.Exists(GetDataFilePath(date));
            if (exists)
            {
                string[] splitOrder;
                string orderString;
                using (var stream = File.OpenRead(GetDataFilePath(date)))
                using (var reader = new StreamReader(stream))
                {
                    while (reader.EndOfStream != true)
                    {

                        var order = new Order();
                        orderString = reader.ReadLine();
                        splitOrder = orderString.Split(',');
                        int orderNumber;
                        int.TryParse(splitOrder[0], out orderNumber);
                        order.orderNumber = orderNumber;
                        order.date = date;
                        order.customerName = splitOrder[1];
                        decimal taxRate;
                        decimal.TryParse(splitOrder[3], out taxRate);
                        decimal materialCostPerFoot;
                        decimal laborCostPerFoot;
                        decimal.TryParse(splitOrder[6], out materialCostPerFoot);
                        decimal.TryParse(splitOrder[7], out laborCostPerFoot);
                        order.stateTax = new StateTax(splitOrder[2], taxRate);
                        order.product = new Product(splitOrder[4], materialCostPerFoot, laborCostPerFoot);
                        decimal area;
                        decimal.TryParse(splitOrder[5], out area);
                        order.area = area;
                        decimal materialCost;
                        decimal.TryParse(splitOrder[8], out materialCost);
                        decimal laborCost;
                        decimal.TryParse(splitOrder[9], out laborCost);
                        order.materialCost = materialCost;
                        order.laborCost = laborCost;
                        decimal totalTax;
                        decimal total;
                        decimal.TryParse(splitOrder[10], out totalTax);
                        decimal.TryParse(splitOrder[11], out total);
                        order.totalTax = totalTax;
                        order.totalCost = total;
                        orders.Add(order);
                    }
                }
                
            }
            return orders;
        }

        private string GetDataFilePath(DateTime identifier)
        {
            string date = identifier.ToString("MMddyyyy");
            return string.Format(@"C:\Users\apprentice\Desktop\repos\justin-gordon-individual-work\FlooringOrders\TextOrderFiles\Orders_{0}.txt", date);
        }

        private void WriteToFile(List<Order> orders, DateTime date)
        {
            orders.OrderBy(order => order.orderNumber);
            File.Delete(GetDataFilePath(date));
            foreach (Order o in orders)
            {
                File.AppendAllText(GetDataFilePath(o.date), (o.ToStringForFile() + Environment.NewLine));
            }
        }
    }
}
