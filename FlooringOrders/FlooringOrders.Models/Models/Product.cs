using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models
{
    public class Product
    {
        public string productType { get; set; }
        public decimal costPerSquareFoot { get; set; }
        public decimal laborCostPerSquareFoot { get; set; }

        public Product(string product, decimal materials, decimal labor)
        {
            productType = product;
            costPerSquareFoot = materials;
            laborCostPerSquareFoot = labor;
        }
    }
}
