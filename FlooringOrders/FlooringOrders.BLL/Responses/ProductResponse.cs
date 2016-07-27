using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Responses
{
    public class ProductResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Product Product { get; set; }
    }
}
