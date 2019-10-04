using FlooringOrder.Data;
using System;
using System.Configuration;

namespace FlooringOrder.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new OrderManager(new TestOrderRepository(), new TestProductRepository(), new TestTaxRepository());
                case "Prod":
                    return new OrderManager(new ProdOrderRepository(), new ProdProductRepository(), new ProdTaxRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
