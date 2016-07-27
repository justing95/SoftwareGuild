using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringOrders.Data;

namespace FlooringOrders.UI
{
    public static class Factory
    {
        public static IOrderRepository GetOrderRepo()
        {
            if (IsTest())
            {
                return new MemoryOrderRepository();
            }
            else
            {
                return new FileOrderRepository();
            }
        }

        public static IProductRepository GetProductRepo()
        {
            if (IsTest())
            {
                return new MemoryProductRepository();
            }
            else
            {
                return new FileProductRepository();
            }

        }

        public static IStateTaxRepository GetTaxRepo()
        {
            if (IsTest())
            {
                return new MemoryTaxRepository();
            }
            else
            {
                return new FileTaxRepository();
            }
        }

        private static bool IsTest()
        {
            return ConfigurationManager.AppSettings["Mode"] == "TEST";
        }
    }
}
