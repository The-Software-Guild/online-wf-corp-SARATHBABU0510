using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FlooringOrder.Data
{
    public class TestTaxRepository : ITaxRepository
    {
        private static List<Tax> _taxList = new List<Tax>
        {
            new Tax
            {
                StateAbbreviation   = "MI",
                StateName   = "Michigan",
                TaxRate   = 5.75M
            },
            new Tax
            {
                StateAbbreviation   = "OH",
                StateName   = "Ohio",
                TaxRate   = 6.25M
            },
            new Tax
            {
                StateAbbreviation   = "IN",
                StateName   = "Indiana",
                TaxRate   = 6.00M
            }
         };
        public Tax LoadTax(string StateAbbreviation)
        {
            Tax tax = new Tax();
            tax = _taxList.FirstOrDefault(a => a.StateAbbreviation == StateAbbreviation);
            return tax;
        }
        public List<Tax> LoadTaxes()
        {
            return _taxList;
        }
    }
}
