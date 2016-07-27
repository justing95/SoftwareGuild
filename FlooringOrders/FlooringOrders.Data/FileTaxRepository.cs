using FlooringOrders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.Data
{
    public class FileTaxRepository : IStateTaxRepository
    {
        public List<StateTax> GetAllTaxes()
        {
            List<StateTax> allTaxes = new List<StateTax>();
            bool exists = File.Exists(GetDataFilePath());
            if (exists)
            {

                string[] splitTaxes;
                string taxString;
                using (var stream = File.OpenRead(GetDataFilePath()))
                using (var reader = new StreamReader(stream))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        taxString = reader.ReadLine();
                        splitTaxes = taxString.Split(',');
                        string state = splitTaxes[0];
                        decimal tax;
                        decimal.TryParse(splitTaxes[2], out tax);
                        StateTax newTax = new StateTax(state, tax);
                        allTaxes.Add(newTax);
                    }
                }
            }
            return allTaxes;
        }
        public StateTax GetTaxes(string stateAbbreviation)
        {
            StateTax tax = null;
            List<StateTax> taxes = GetAllTaxes();
            foreach(StateTax t in taxes)
            {
                if (t.stateAbbreviation == stateAbbreviation)
                {
                    tax = t;
                }
            }
            return tax;
        }

        private string GetDataFilePath()
        {
            return (@"C:\Users\apprentice\Desktop\repos\justin-gordon-individual-work\FlooringOrders\ProductsAndTaxes\Taxes.txt");
        }
    }
}
