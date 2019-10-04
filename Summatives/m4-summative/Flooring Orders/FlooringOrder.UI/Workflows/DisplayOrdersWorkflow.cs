using FlooringOrder.BLL;
using FlooringOrder.Models;
using FlooringOrder.Models.Responses;
using System;

namespace FlooringOrder.UI.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Lookup an Order(s)");
            Console.WriteLine("--------------------------");
            string orderdate = ConsoleIO.EnterOrderdate("Enter an order date in mm/dd/yyyy format, example 01/12/2015: ");

            OrdersLookupResponse orderslookupresponse = manager.LookupOrders(orderdate);
            if (orderslookupresponse.Success)
            {
                foreach (Order order in orderslookupresponse.Orderlist)
                {
                    ConsoleIO.DisplayOrderDetails(order, orderdate);
                }
            }
            else
            {
                Console.WriteLine(orderslookupresponse.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
