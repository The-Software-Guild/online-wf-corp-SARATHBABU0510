using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace FlooringOrder.Data
{
    public class TestProductRepository : IProductRepository
    {
        private static List<Product> _productList = new List<Product>
        {
            new Product
            {
                ProductType  = "Wood",
                CostPerSquareFoot  = 5.15M,
                LaborCostPerSquareFoot  = 4.75M
            },
            new Product
            {
                ProductType  = "Tile",
                CostPerSquareFoot  = 3.5M,
                LaborCostPerSquareFoot  = 4.15M
            },
            new Product
            {
                ProductType  = "Carpet",
                CostPerSquareFoot  = 2.25M,
                LaborCostPerSquareFoot  = 2.10M
            }
         };
        public Product LoadProduct(string ProductType)
        {
            Product product = new Product();
            product = _productList.FirstOrDefault(p => p.ProductType == ProductType);
            return product;
        }
        public List<Product> LoadProducts()
        {
            return _productList;
        }
    }
}
