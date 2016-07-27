using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class MemoryTaxRepository : IStateTaxRepository
    {
        List<StateTax> taxes = new List<StateTax>();
        public MemoryTaxRepository()
        {
            StateTax tax1 = new StateTax("MN", 5.0m);
            StateTax tax2 = new StateTax("TX", 1.0m);
            StateTax tax3 = new StateTax("CA", 6.0m);
            taxes.Add(tax1);
            taxes.Add(tax2);
            taxes.Add(tax3);
        }

        public StateTax GetTaxes(string stateAbbreviation)
        {
            StateTax tax = null;
            foreach (StateTax t in taxes)
            {
                if (t.stateAbbreviation == stateAbbreviation)
                {
                    tax = t;
                }
            }
            return tax;
        }
    }
}
