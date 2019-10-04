using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlooringOrder.Data
{
    public class ProdProductRepository : IProductRepository
    {
        private static readonly string productspath = "Products.txt";
        private static List<Product> _productList = new List<Product>();
        public Product LoadProduct(string ProductType)
        {
            Product product = new Product();
            BuildProductListFromFile();
            product = _productList.FirstOrDefault(p => p.ProductType == ProductType);            
            return product;
        }
        public List<Product> LoadProducts()
        {
            BuildProductListFromFile();            
            return _productList;
        }
        private void BuildProductListFromFile()
        {
            _productList.Clear();
            if (File.Exists(productspath))
            {
                List<string> rows = new List<string>();
                using (StreamReader reader = new StreamReader(productspath))
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
                        Product _product = new Product();
                        _product.ProductType = columns[0];
                        _product.CostPerSquareFoot = Convert.ToDecimal(columns[1]);
                        _product.LaborCostPerSquareFoot = Convert.ToDecimal(columns[2]);
                        _productList.Add(_product);
                    }
                }
            }
        }     
    }
}