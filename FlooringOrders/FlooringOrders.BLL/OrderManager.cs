using FlooringOrders.BLL.Responses;
using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL
{
    public class OrderManager
    {
        List<string> errors;
        IOrderRepository repo;
        public OrderManager(IOrderRepository repo)
        {
            this.repo = repo;
            errors = new List<string>();
        }

        public List<Order> GetOrders(DateTime date)
        {
            return repo.GetOrders(date);
        }

        public void AddOrder(Order order)
        {
            repo.AddOrder(order);
        }

        public OrderResponse DeleteOrder(DateTime date, int orderNumber)
        {

            bool success = repo.DeleteOrder(date, orderNumber);
            OrderResponse response = new OrderResponse();
            response.Success = success;
            if (success)
            {
                response.Message = "Successfully deleted the order";
            }
            else
            {
                response.Message = "Couldn't find order to delete";
                AddToErrors(response.Message);
            }
            return response;
        }

        public OrderResponse EditOrder(DateTime date, int orderNumber)
        {
            Order order = repo.EditOrder(date, orderNumber);
            OrderResponse response = new OrderResponse();
            if (order == null)
            {
                response.Success = false;
                response.Message = "Couldn't find order to edit";
                AddToErrors(response.Message);
            }
            else
            {
                response.Success = true;
                response.Message = "Succesfully found Order";
                response.Order = order;
            }
            return response;
        }

        public void AddToErrors(string error)
        {
            errors.Add(error);
            string errorFile = (@"C:\Users\apprentice\Desktop\repos\justin-gordon-individual-work\FlooringOrders\TextOrderFiles\Errors.txt");
            File.Delete(errorFile);
            foreach (string e in errors)
            {
                File.AppendAllText(errorFile, e + Environment.NewLine);
            }

        }
    }
}
