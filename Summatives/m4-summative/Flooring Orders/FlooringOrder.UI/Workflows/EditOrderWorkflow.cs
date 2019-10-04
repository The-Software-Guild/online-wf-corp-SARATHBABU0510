using FlooringOrder.BLL;
using FlooringOrder.Models;
using FlooringOrder.Models.Responses;
using System;

namespace FlooringOrder.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            OrderManager manager = OrderManagerFactory.Create();
            
            string orderdate = ConsoleIO.EnterOrderdate("Enter an order date in mm/dd/yyyy format, example 01/12/2015: ");
            int ordernumber  = ConsoleIO.EnterOrderNumber("Enter an order number, example 5 or 10: ");

            OrderLookupResponse orderlookupresponse = manager.LookupOrder(orderdate, ordernumber);
            if (orderlookupresponse.Success)
            {
                Order order = orderlookupresponse.order;
                Console.WriteLine("Enter the new order information: ");

                string customername = ConsoleIO.EnterCustomerName($"Enter the Customer Name ({order.CustomerName}): ", OrderActionType.Edit);
                customername = customername == "" ? order.CustomerName : customername;                

                TaxesLookupResponse taxeslookupresponse = manager.LookupTaxes();
                string state = ConsoleIO.EnterState($"Enter the State(abbreviated form) ({order.State}): ", taxeslookupresponse.taxlist, OrderActionType.Edit);
                state = state == "" ? order.State : state;

                ProductsLookupResponse productslookupresponse = manager.LookupProducts();
                string producttype = ConsoleIO.EnterProductType($"Enter the Product Type from the above ({order.ProductType}): ", productslookupresponse.productlist, OrderActionType.Edit);
                producttype = producttype == "" ? order.ProductType : producttype;

                decimal area = ConsoleIO.EnterArea($"Minimum order size is 100 square feet. Enter the area ({order.Area}): ", OrderActionType.Edit);
                area = area == 0.00M ? order.Area : area;                

                order = manager.GenerateOrderInfo(ordernumber, customername, state, producttype, area);
                Console.WriteLine("Below is the updated order Summary: ");
                ConsoleIO.DisplayOrderDetails(order, orderdate);

                char confirmorder = ConsoleIO.UserchoiceYesorNo("Are you sure you want to update the order? Enter Y or N: ");
                if (char.ToUpper(confirmorder) == 'Y')
                {
                    Response editorder = manager.ProcessOrder(order, orderdate, OrderActionType.Edit);
                    if (editorder.Success)
                    {
                        Console.WriteLine("Order updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Order not updated. Please try again or contact IT!");
                    }
                }
            }
            else
            {
                Console.WriteLine(orderlookupresponse.Message);
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }        
    }
}
