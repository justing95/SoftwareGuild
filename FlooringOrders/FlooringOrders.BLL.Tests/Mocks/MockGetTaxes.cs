using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests.Mocks
{
    class MockGetTaxes : IStateTaxRepository
    {
        public StateTax GetTaxes(string stateAbbreviation)
        {
            StateTax tax = new StateTax("MN", 8.0m);
            return tax;
        }
    }
}
