using FlooringOrder.BLL;
using FlooringOrder.Models;
using FlooringOrder.Models.Responses;
using System;

namespace FlooringOrder.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();

            Console.WriteLine("Enter the new order information: ");
            string orderdate = ConsoleIO.EnterFutureOrderdate("Enter a future order date in mm/dd/yyyy format, example 01/12/2020: ");
            string customername = ConsoleIO.EnterCustomerName("Enter the Customer Name: ", OrderActionType.Add);
            TaxesLookupResponse taxeslookupresponse = manager.LookupTaxes();
            string state = ConsoleIO.EnterState("Enter the State(abbreviated form): ", taxeslookupresponse.taxlist, OrderActionType.Add);
            ProductsLookupResponse productslookupresponse = manager.LookupProducts();
            string producttype = ConsoleIO.EnterProductType("Enter the Product Type from the above: ", productslookupresponse.productlist, OrderActionType.Add);
            decimal area = ConsoleIO.EnterArea("Minimum order size is 100 square feet. Enter the area: ", OrderActionType.Add);

            int ordernumber = manager.Generateordernumber(orderdate);
            Order order = manager.GenerateOrderInfo(ordernumber, customername, state, producttype, area);
            Console.WriteLine("Below is the New order Summary that you just entered: ");
            ConsoleIO.DisplayOrderDetails(order, orderdate);

            char confirmorder = ConsoleIO.UserchoiceYesorNo("Are you sure you want to place the order? Enter Y or N: ");
            if (char.ToUpper(confirmorder) == 'Y')
            {
                Response addorder = manager.ProcessOrder(order, orderdate, OrderActionType.Add);
                if (addorder.Success)
                {
                    Console.WriteLine("New Order saved successfully!");
                }
                else
                {
                    Console.WriteLine("New Order not saved. Please try again or contact IT!");
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }        
    }
}
