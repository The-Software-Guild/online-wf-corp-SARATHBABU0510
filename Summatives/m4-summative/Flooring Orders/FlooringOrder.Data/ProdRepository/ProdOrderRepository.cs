using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using FlooringOrder.Models.Responses;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;


namespace FlooringOrder.Data
{
    public class ProdOrderRepository : IOrderRepository
    {
        private static readonly string ordersdirpath = "Orders";
        private static readonly string ordersfilesearchbeginstr = "Orders_";
        private static string ordersfilesearchyear = "";
        private static readonly string ordersfilesearchendstr = ".txt";
        private static string ordersheader = "OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total";
        private static List<Order> _orderList = new List<Order>();
        readonly string comma = ",";

        public OrderDateFileResponse BuildPath(string orderdate)
        {
            OrderDateFileResponse orderdatefile = new OrderDateFileResponse();
            orderdate = orderdate.Replace("/", "");
            ordersfilesearchyear = orderdate;
            var ordersfilesearch = (ordersfilesearchbeginstr + ordersfilesearchyear + ordersfilesearchendstr);
            orderdatefile.path = ordersdirpath + @"\" + ordersfilesearch;
            return orderdatefile;
        }
        public OrderDateFileResponse GetPath(string orderdate)
        {
            OrderDateFileResponse orderdatefile = new OrderDateFileResponse();
            orderdate = orderdate.Replace("/", "");
            ordersfilesearchyear = orderdate;
            var ordersfilesearch = (ordersfilesearchbeginstr + ordersfilesearchyear + ordersfilesearchendstr);
            string[] filepath = Directory.GetFiles(ordersdirpath, ordersfilesearch);
            if (filepath.Length > 0)
            {
                orderdatefile.path = filepath[0];
            }
            return orderdatefile;
        }
        public Order LoadOrder(string orderdatepath, int ordernumber)
        {
            Order order = new Order();
            LoadOrders(orderdatepath);
            order = _orderList.FirstOrDefault(o => o.OrderNumber == ordernumber);
            return order;
        }
        public List<Order> LoadOrders(string orderdatepath)
        {
            BuildOrderListFromFile(orderdatepath);
            return _orderList;
        }
        public Order AddOrderToExistingOrderDateFile(Order order, string orderdatepath)
        {
            using (StreamWriter writer = File.AppendText(orderdatepath))
            {
                writer.WriteLine(order.OrderNumber + comma + order.CustomerName + comma + order.State + comma + order.TaxRate
                        + comma + order.ProductType + comma + order.Area + comma + order.CostPerSquareFoot + comma +
                        order.LaborCostPerSquareFoot + comma + order.MaterialCost + comma + order.LaborCost + comma +
                        order.Tax + comma + order.Total);
            }
            return order;
        }
        public Order AddOrderToNewOrderDateFile(Order order, string orderdatepath)
        {
            using (StreamWriter writer = new StreamWriter(orderdatepath))
            {
                writer.WriteLine(ordersheader);
                writer.WriteLine(order.OrderNumber + comma + order.CustomerName + comma + order.State + comma + order.TaxRate
                        + comma + order.ProductType + comma + order.Area + comma + order.CostPerSquareFoot + comma +
                        order.LaborCostPerSquareFoot + comma + order.MaterialCost + comma + order.LaborCost + comma +
                        order.Tax + comma + order.Total);
            }
            return order;
        }

        public Order EditOrder(Order order, string orderdatepath)
        {
            BuildOrderListFromFile(orderdatepath);
            var index = _orderList.FindIndex(o => o.OrderNumber == order.OrderNumber);
            _orderList[index] = order;
            WriteListToOrderFile(_orderList, orderdatepath);
            return order;
        }
        public Order RemoveOrder(Order order, string orderdatepath)
        {
            BuildOrderListFromFile(orderdatepath);
            var removeorder = _orderList.Find(o => o.OrderNumber == order.OrderNumber);
            _orderList.Remove(removeorder);
            WriteListToOrderFile(_orderList, orderdatepath);
            return order;
        }
        public void WriteListToOrderFile(List<Order> orderlist, string orderdatepath)
        {
            using (StreamWriter writer = new StreamWriter(orderdatepath))
            {
                writer.WriteLine(ordersheader);
                foreach (var order in orderlist)
                {
                    writer.WriteLine(order.OrderNumber + comma + order.CustomerName + comma + order.State + comma + order.TaxRate
                        + comma + order.ProductType + comma + order.Area + comma + order.CostPerSquareFoot + comma +
                        order.LaborCostPerSquareFoot + comma + order.MaterialCost + comma + order.LaborCost + comma +
                        order.Tax + comma + order.Total);
                }
            }
        }                   
        private void BuildOrderListFromFile(string orderdatepath)
        {
            _orderList.Clear();
            if (File.Exists(orderdatepath))
            {
                List<string> rows = new List<string>();                
                using (StreamReader reader = new StreamReader(orderdatepath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        rows.Add(line);
                    }
                }
                if (rows.Count > 1)
                {
                    ordersheader = rows[0];
                    for (int i = 1; i < rows.Count; i++)
                    {
                        string[] columns = rows[i].Split(',');
                        Order _order = new Order
                        {
                            OrderNumber = Convert.ToInt32(columns[0]),
                            CustomerName = columns[1],
                            State = columns[2],
                            TaxRate = Convert.ToDecimal(columns[3]),
                            ProductType = columns[4],
                            Area = Convert.ToDecimal(columns[5]),
                            CostPerSquareFoot = Convert.ToDecimal(columns[6]),
                            LaborCostPerSquareFoot = Convert.ToDecimal(columns[7]),
                            MaterialCost = Convert.ToDecimal(columns[8]),
                            LaborCost = Convert.ToDecimal(columns[9]),
                            Tax = Convert.ToDecimal(columns[10]),
                            Total = Convert.ToDecimal(columns[11])
                        };
                        _orderList.Add(_order);
                    }
                }
            }
        }
    }
}
