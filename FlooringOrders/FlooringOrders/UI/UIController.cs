using FlooringOrders.BLL;
using FlooringOrders.BLL.Responses;
using FlooringOrders.Data;
using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.UI
{
    public class UIController
    {
        IOrderRepository fileRepo;
        OrderManager orderManager;
        ProductManager productManager;
        IProductRepository productRepo;
        IStateTaxRepository taxRepo;
        TaxManager taxManager;

        public UIController()
        {
            fileRepo = new FileOrderRepository();
            orderManager = new OrderManager(fileRepo);
            productRepo = new FileProductRepository();
            productManager = new ProductManager(productRepo);
            taxRepo = new FileTaxRepository();
            taxManager = new TaxManager(taxRepo);

        }
        public void Start()
        {
            int choice = Menu();
            while (choice != 5)
            {
                if (choice == 1)
                {
                    DisplayOrders();
                }
                if (choice == 2)
                {
                    AddOrder();
                }
                if (choice == 3)
                {
                    EditOrder();
                }
                if (choice == 4)
                {
                    DeleteOrder();
                }
                choice = Menu();
            }
        }

        public int Menu()
        {
            Console.WriteLine("**********************************");
            Console.WriteLine("*        Flooring Orders");
            Console.WriteLine("*");
            Console.WriteLine("* 1. Display Orders");
            Console.WriteLine("* 2. Add an Order");
            Console.WriteLine("* 3. Edit an Order");
            Console.WriteLine("* 4. Delete an Order");
            Console.WriteLine("* 5. Quit");
            Console.WriteLine("*");
            Console.WriteLine("**********************************");
            Console.Write("User Choice: ");
            string userChoice = Console.ReadLine();
            int choice;
            int.TryParse(userChoice, out choice);
            while (choice != 1 && choice != 2 && choice != 3 && choice != 4 & choice != 5)
            {
                Console.Write("Input a number between 1 and 5: ");
                userChoice = Console.ReadLine();
                int.TryParse(userChoice, out choice);
            }
            return choice;

        }
        public void DisplayOrders()
        {
            DateTime date = InputDate();
            List<Order> orders = orderManager.GetOrders(date);
            Console.WriteLine();
            foreach (Order order in orders)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine(order);
            }
            Console.WriteLine("-----------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public void AddOrder()
        {
            string confirm = "2";
            while (confirm == "2")
            {
                Console.Write("What is the customers name? ");
                string customerName = Console.ReadLine();
                while (customerName == "")
                {
                    Console.Write("Please input a name: ");
                    customerName = Console.ReadLine();
                }
                DateTime date = InputDate();
                Console.Write("What state is the customer from? (2 letter abbreviation): ");
                string stateAbbreviation = Console.ReadLine().ToUpper();
                while (stateAbbreviation.Length != 2)
                {
                    Console.Write("Please input a 2 letter state abbreviation: ");
                    stateAbbreviation = Console.ReadLine().ToUpper();
                }
                TaxResponse taxResponse = taxManager.GetTaxes(stateAbbreviation);
                if (!taxResponse.Success)
                {
                    orderManager.AddToErrors(taxResponse.Message);
                    Console.WriteLine(taxResponse.Message);
                    Console.ReadKey();
                    return;
                }
                StateTax newTax = taxResponse.StateTax;
                Console.Write("What product did the customer order? ");
                string productType = Console.ReadLine();
                while (productType == "")
                {
                    Console.Write("Please input a product type: ");
                    productType = Console.ReadLine();
                }
                ProductResponse productResponse = productManager.GetProduct(productType);
                Product newProduct = productResponse.Product;
                if (!productResponse.Success)
                {
                    orderManager.AddToErrors(productResponse.Message);
                    Console.WriteLine(productResponse.Message);
                    Console.ReadKey();
                    return;
                }
                Console.Write("How many square feet did the customer order? ");
                decimal area;
                string userArea = Console.ReadLine();
                while (!decimal.TryParse(userArea, out area) || area <= 0)
                {
                    Console.Write("input a valid number: ");
                    userArea = Console.ReadLine();
                }
                Console.WriteLine("\nCustomer name- " + customerName);
                Console.WriteLine("Date- " + date);
                Console.WriteLine("Customer state- " + stateAbbreviation);
                Console.WriteLine("Product type- " + productType);
                Console.WriteLine("Area- " + userArea);
                Console.WriteLine("Is this information correct?");
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                confirm = Console.ReadLine();
                while (confirm != "1" && confirm != "2")
                {
                    Console.Write("input a 1 or 2: ");
                    confirm = Console.ReadLine();
                }
                if (confirm == "1")
                {
                    Order newOrder = new Order(customerName, newProduct, newTax, area, date);
                    orderManager.AddOrder(newOrder);
                }
            }
        }

        public void EditOrder()
        {
            DateTime date = InputDate();
            int orderNumber = InputOrderNumber();
            OrderResponse response = orderManager.EditOrder(date, orderNumber);
            if (!response.Success)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
                return;
            }
            Order edit = response.Order;
            Console.WriteLine(string.Format("Would you like to change the customer name? ({0}) ", edit.customerName));
            string userChoice = Console.ReadLine();
            string newName;
            if (userChoice == "")
            {
                newName = edit.customerName;
            }
            else
            {
                newName = userChoice;
            }
            Console.WriteLine(string.Format("Would you like to change the date? (MM/DD/YYY) ({0}) ", edit.date));
            DateTime newDate;
            string userDate = Console.ReadLine();
            if (userDate == "")
            {
                newDate = edit.date;
            }
            else
            {
                while (!DateTime.TryParse(userDate, out newDate))
                {
                    Console.Write("Incorrect Format: Please input a date (MM/DD/YYYY): ");
                    userDate = Console.ReadLine();
                }
            }
            Console.WriteLine(string.Format("Would you like to change the state? ({0}) ", edit.stateTax.stateAbbreviation));
            userChoice = Console.ReadLine();
            StateTax newState;
            if (userChoice == "")
            {
                newState = edit.stateTax;
            }
            else
            {
                while (userChoice.Length != 2)
                {
                    Console.WriteLine("Please enter a 2 letter state abbreviation");
                    userChoice = Console.ReadLine();
                }
                TaxResponse taxResponse = taxManager.GetTaxes(userChoice);
                if (!taxResponse.Success)
                {
                    orderManager.AddToErrors(taxResponse.Message);
                    Console.WriteLine(taxResponse.Message);
                    Console.ReadKey();
                    return;
                }
                newState = taxResponse.StateTax;
            }
            Console.WriteLine(string.Format("Would you like to change the product? ({0})", edit.product.productType));
            userChoice = Console.ReadLine();
            Product newProduct;
            if (userChoice == "")
            {
                newProduct = edit.product;
            }
            else
            {
                ProductResponse productResponse = productManager.GetProduct(userChoice);
                if (!productResponse.Success)
                {
                    orderManager.AddToErrors(productResponse.Message);
                    Console.WriteLine(productResponse.Message);
                    Console.ReadKey();
                    return;
                }
                newProduct = productResponse.Product;
            }
            Console.WriteLine(string.Format("Would you like to change the area? ({0}) ", edit.area));
            userChoice = Console.ReadLine();
            decimal newArea;
            if (userChoice == "")
            {
                newArea = edit.area;
            }
            else
            {
                while (!decimal.TryParse(userChoice, out newArea) || newArea <= 0)
                {
                    Console.WriteLine("Input a valid number");
                    userChoice = Console.ReadLine();
                }
            }
            Order newOrder = new Order(newName, newProduct, newState, newArea, newDate);
            newOrder.orderNumber = orderNumber;
            orderManager.DeleteOrder(date, orderNumber);
            orderManager.AddOrder(newOrder);

        }

        public void DeleteOrder()
        {
            DateTime date = InputDate();
            int orderNumber = InputOrderNumber();
            OrderResponse response = orderManager.DeleteOrder(date, orderNumber); 
            if (!response.Success)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
        }

        public DateTime InputDate()
        {
            Console.Write("Input a date (MM/DD/YYYY): ");
            DateTime date;
            string userDate = Console.ReadLine();
            while (!DateTime.TryParse(userDate, out date))
            {
                Console.Write("Incorrect Format: Please input a date (MM/DD/YYYY): ");
                userDate = Console.ReadLine();
            }
            return date;
        }

        public int InputOrderNumber()
        {
            Console.Write("Input the order number: ");
            string userOrderNumber = Console.ReadLine();
            int orderNumber;
            while (!int.TryParse(userOrderNumber, out orderNumber))
            {
                Console.Write("Input a valid number: ");
                userOrderNumber = Console.ReadLine();
            }
            return orderNumber;
        }

    }
}
