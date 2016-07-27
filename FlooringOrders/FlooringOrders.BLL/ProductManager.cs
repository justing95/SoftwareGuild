using FlooringOrders.BLL.Responses;
using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL
{
    public class ProductManager
    {
        IProductRepository repo;
        public ProductManager(IProductRepository repo)
        {
            this.repo = repo;
        }
        public ProductResponse GetProduct(string productType)
        {
            Product product = repo.GetProduct(productType);
            ProductResponse response = new ProductResponse();
            if (product == null)
            {
                response.Success = false;
                response.Message = ("Do not have information for product type: " + productType);
            }
            else
            {
                response.Success = true;
                response.Product = product;
            }
            return response;
        }
    }
}
