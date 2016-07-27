using FlooringOrders.BLL.Responses;
using FlooringOrders.BLL.Tests.Mocks;
using FlooringOrders.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringOrders.BLL.Tests
{
    [TestFixture]
    class TaxManagerTests
    {
        [Test]
        public void CanGetTaxes()
        {
            IStateTaxRepository repo = new MockGetTaxes();
            TaxManager taxManager = new TaxManager(repo);
            TaxResponse response = taxManager.GetTaxes("MN");
            StateTax tax = response.StateTax;
            Assert.IsNotNull(tax);
        }

        [Test]
        public void GetTaxesFails()
        {
            IStateTaxRepository repo = new MockInvalidStateTax();
            TaxManager taxManager = new TaxManager(repo);
            TaxResponse response = taxManager.GetTaxes("MN");
            Assert.AreEqual(false, response.Success);
        }
    }
}
