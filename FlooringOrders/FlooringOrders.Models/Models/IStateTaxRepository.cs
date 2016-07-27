using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Models
{
    public interface IStateTaxRepository
    {
        StateTax GetTaxes(string stateAbbreviation);

    }
}
