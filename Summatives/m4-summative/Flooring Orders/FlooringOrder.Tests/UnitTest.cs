using FlooringOrder.BLL;
using FlooringOrder.Models;
using FlooringOrder.Models.Responses;
using NUnit.Framework;

namespace FlooringOrder.Tests
{
    [TestFixture]
    public class UnitTest
    {
        [TestCase("06/06/2013", 1, true, "")]
        [TestCase("05/05/2020", 4, true, "")]
        [TestCase("06/06/2013", 3, false, "No orders with order date 06/06/2013 and order number 3. Please enter another order date and or order number or contact IT!")]
        [TestCase("05/05/2020", 1, false, "No orders with order date 05/05/2020 and order number 1. Please enter another order date and or order number or contact IT!")]
        [TestCase("01/01/2030", 1, false, "No orders with order date 01/01/2030 and order number 1. Please enter another order date and or order number or contact IT!")]
        [TestCase("01/01/2040", 1, false, "File not found for Order date 01/01/2040 or some problem. Please contact IT.")]
        [Test]
        public void LookupOrderTest(string orderdate, int ordernumber, bool expectedSuccess, string expectedmessage)
        {
            OrderManager manager = OrderManagerFactory.Create();
            OrderLookupResponse orderlookupresponse = manager.LookupOrder(orderdate, ordernumber);
            Assert.AreEqual(expectedSuccess, orderlookupresponse.Success);
            if (orderlookupresponse.Success == false)
            {
                Assert.AreEqual(expectedmessage, orderlookupresponse.Message);
            }
        }

        [TestCase("06/06/2013", true, "")]
        [TestCase("05/05/2020", true, "")]
        [TestCase("01/01/2030", false, "No orders with 01/01/2030 or problem with the file. Please enter another order date or contact IT!")]
        [TestCase("01/01/2040", false, "File not found for Order date 01/01/2040 or some problem. Please contact IT.")]
        [Test]
        public void LookupOrdersTest(string orderdate, bool expectedSuccess, string expectedmessage)
        {
            OrderManager manager = OrderManagerFactory.Create();
            OrdersLookupResponse orderslookupresponse = manager.LookupOrders(orderdate);
            Assert.AreEqual(expectedSuccess, orderslookupresponse.Success);
            if (orderslookupresponse.Success == false)
            {
                Assert.AreEqual(expectedmessage, orderslookupresponse.Message);
            }
        }

        [TestCase("06/06/2014", OrderActionType.Add, true,"")]
        [TestCase("06/06/2013", OrderActionType.Edit, true,"")]
        [TestCase("06/06/2013", OrderActionType.Remove, true,"")]
        [TestCase("06/06/2013", OrderActionType.Add, true,"")]
        [TestCase("06/06/201", OrderActionType.Add, false, "Problem creating a new order date file for Order date 06/06/201. Please contact IT.")]
        [TestCase("06/06/201", OrderActionType.Edit, false, "File not found for Order date 06/06/201 or some problem. Please contact IT.")]
        [TestCase("06/06/201", OrderActionType.Remove, false, "File not found for Order date 06/06/201 or some problem. Please contact IT.")]
        
        [Test]
        public void ProcessOrderTest(string orderdate, OrderActionType orderactionype, bool expectedSuccess,string expectedmessage)
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order
            {
                OrderNumber = 1,
                CustomerName = "Wise Edit",
                State = "OH",
                TaxRate = 6.25M,
                ProductType = "Wood",
                Area = 100.00M,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M,
                MaterialCost = 515.00M,
                LaborCost = 475.00M,
                Tax = 61.875M,
                Total = 1051.875M
            };
            ProcessOrderResponse processorderresponse = manager.ProcessOrder(order, orderdate, orderactionype);
            Assert.AreEqual(expectedSuccess, processorderresponse.Success);
            if (processorderresponse.Success == true)
            {
                Assert.AreEqual(order, processorderresponse.order);
            }
            else
            {
                Assert.AreEqual(expectedmessage, processorderresponse.Message);
            }
        }

        [TestCase(true)]
        [Test]
        public void LookupProductsTest(bool expectedSuccess)
        {
            OrderManager manager = OrderManagerFactory.Create();
            ProductsLookupResponse productslookupresponse = manager.LookupProducts();
            Assert.AreEqual(expectedSuccess, productslookupresponse.Success);
        }

        [TestCase(true)]
        [Test]
        public void LookupTaxesTest(bool expectedSuccess)
        {
            OrderManager manager = OrderManagerFactory.Create();
            TaxesLookupResponse taxeslookupresponse = manager.LookupTaxes();
            Assert.AreEqual(expectedSuccess, taxeslookupresponse.Success);
        }

        [TestCase("06/06/2013", 3)]
        [TestCase("05/05/2020", 5)]
        [TestCase("01/01/2020", 1)]
        [Test]
        public void GenerateOrderNumberTest(string orderdate, int expectedordernumber)
        {
            OrderManager manager = OrderManagerFactory.Create();
            int resultedordernumber = manager.Generateordernumber(orderdate);
            Assert.AreEqual(expectedordernumber, resultedordernumber);
        }

        [TestCase(1, "Wise", "OH", "Wood", 100.00, 515.00, 475.00, 61.875, 1051.875,6.25,5.15,4.75)]
        [TestCase(1, "Wise", "MI", "Tile", 150.00, 525.00, 622.50, 65.98125, 1213.48125,5.75,3.5,4.15)]
        [TestCase(1, "Wise", "IN", "Carpet", 200.00, 450.00, 420.00, 52.20, 922.20,6.00,2.25,2.10)]
        [Test]
        public void GenerateOrderInfoTest(int orderNumber, string customername, string state, string producttype, decimal area, decimal materialcost, decimal laborcost, decimal tax, decimal total,decimal taxrate, decimal costPerSquareFoot, decimal laborCostPerSquareFoot)
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order
            {
                OrderNumber = orderNumber,
                CustomerName = customername,
                State = state,
                TaxRate = taxrate,
                ProductType = producttype,
                Area = area,
                CostPerSquareFoot = costPerSquareFoot,
                LaborCostPerSquareFoot = laborCostPerSquareFoot,
                MaterialCost = materialcost,
                LaborCost = laborcost,
                Tax = tax,
                Total = total
            };
            Order resultedorder = manager.GenerateOrderInfo(orderNumber, customername, state, producttype, area);

            Assert.AreEqual(order.OrderNumber, resultedorder.OrderNumber);
            Assert.AreEqual(order.CustomerName, resultedorder.CustomerName);
            Assert.AreEqual(order.State, resultedorder.State);
            Assert.AreEqual(order.TaxRate, resultedorder.TaxRate);
            Assert.AreEqual(order.ProductType, resultedorder.ProductType);
            Assert.AreEqual(order.Area, resultedorder.Area);
            Assert.AreEqual(order.CostPerSquareFoot, resultedorder.CostPerSquareFoot);
            Assert.AreEqual(order.LaborCostPerSquareFoot, resultedorder.LaborCostPerSquareFoot);
            Assert.AreEqual(order.MaterialCost, resultedorder.MaterialCost);
            Assert.AreEqual(order.LaborCost, resultedorder.LaborCost);
            Assert.AreEqual(order.Tax, resultedorder.Tax);
            Assert.AreEqual(order.Total, resultedorder.Total);
        }
    }
}
