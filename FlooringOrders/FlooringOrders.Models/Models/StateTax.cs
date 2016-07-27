using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models
{
    public class StateTax
    {
        public string stateAbbreviation { get; set; }
        public decimal taxRate { get; set; }

        public StateTax(string state, decimal tax)
        {
            stateAbbreviation = state;
            taxRate = tax;
        }
    }
}
