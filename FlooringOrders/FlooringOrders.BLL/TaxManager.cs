using FlooringOrders.BLL.Responses;
using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL
{
    public class TaxManager
    {
        IStateTaxRepository repo;

        public TaxManager(IStateTaxRepository repo)
        {
            this.repo = repo;
        }

        public TaxResponse GetTaxes(string stateAbbreviation)
        {
            StateTax tax = repo.GetTaxes(stateAbbreviation);
            TaxResponse response = new TaxResponse();
            if (tax == null)
            {
                response.Success = false;
                response.Message = ("Do not have tax information for " + stateAbbreviation);
            }
            else
            {
                response.Success = true;
                response.StateTax = tax;
            }
            return response;
        }
    }
}
