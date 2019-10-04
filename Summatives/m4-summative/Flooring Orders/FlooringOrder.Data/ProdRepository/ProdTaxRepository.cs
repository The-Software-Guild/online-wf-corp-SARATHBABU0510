using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlooringOrder.Data
{
    public class ProdTaxRepository: ITaxRepository
    {
        private static readonly string taxespath = "Taxes.txt";
        private static List<Tax> _taxList = new List<Tax>();
        public Tax LoadTax(string StateAbbreviation)
        {
            Tax tax = new Tax();
            BuildTaxListFromFile();
            tax = _taxList.FirstOrDefault(a => a.StateAbbreviation == StateAbbreviation);            
            return tax;
        }
        public List<Tax> LoadTaxes()
        {
            BuildTaxListFromFile();           
            return _taxList;
        }
        private void BuildTaxListFromFile()
        {
            _taxList.Clear();
            if (File.Exists(taxespath))
            {
                List<string> rows = new List<string>();
                using (StreamReader reader = new StreamReader(taxespath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        rows.Add(line);
                    }
                }
                if (rows.Count > 0)
                {
                    for (int i = 1; i < rows.Count; i++)
                    {
                        string[] columns = rows[i].Split(',');
                        Tax _tax = new Tax();
                        _tax.StateAbbreviation = columns[0];
                        _tax.StateName = columns[1];
                        _tax.TaxRate = Convert.ToDecimal(columns[2]);
                        _taxList.Add(_tax);
                    }
                }
            }
        }          
    }
}
