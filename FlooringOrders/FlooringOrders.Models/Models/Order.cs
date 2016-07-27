using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models
{

    public class Order
    {
        static int numOrders = 0;
        public DateTime date { get; set; }
        public int orderNumber { get; set; }
        public string customerName { get; set; }
        public StateTax stateTax { get; set; }
        public Product product { get; set; }
        public decimal area { get; set; }
        public decimal materialCost { get; set; }
        public decimal laborCost { get; set; }
        public decimal totalTax { get; set; }
        public decimal totalCost { get; set; }

        public Order()
        {
        }
        public Order(string customer, Product product, StateTax state, decimal area, DateTime date)
        {
            customerName = customer;
            this.area = area;
            stateTax = state;
            this.product = product;
            this.date = date;
            orderNumber = GetOrderNumber();
            materialCost = area * product.costPerSquareFoot;
            laborCost = area * product.laborCostPerSquareFoot;
            totalTax = (stateTax.taxRate / 100) * (materialCost + laborCost);
            totalCost = materialCost + laborCost + totalTax;
        }

        public override string ToString()
        {
            string newString = ("Order Number: " + orderNumber + "\nCustomer Name: " + customerName + "\nTotal Cost: $" + totalCost);
            return newString;
        }

        public string ToStringForFile()
        {
            string newString = (orderNumber + "," + customerName
                + "," + stateTax.stateAbbreviation + "," + stateTax.taxRate
                + "," + product.productType + "," + area + "," + product.costPerSquareFoot
                + "," + product.laborCostPerSquareFoot + "," + materialCost + "," + laborCost + "," + totalTax + "," + totalCost);
            return newString;
        }

        private int GetOrderNumber()
        {
            string orderPath = @"C:\Users\apprentice\Desktop\repos\justin-gordon-individual-work\FlooringOrders\TextOrderFiles\ORDERNUMBER.txt";
            string orderFromFile = File.ReadAllText(orderPath);
            int number;
            int.TryParse(orderFromFile, out number);
            WriteOrderNumber(number + 1);
            return number;
        }

        private void WriteOrderNumber(int newNumber)
        {
            string orderPath = @"C:\Users\apprentice\Desktop\repos\justin-gordon-individual-work\FlooringOrders\TextOrderFiles\ORDERNUMBER.txt";
            File.WriteAllText(orderPath, newNumber.ToString());
        }
    }
}
