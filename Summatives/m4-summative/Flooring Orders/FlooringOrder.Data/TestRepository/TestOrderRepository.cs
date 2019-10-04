using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using FlooringOrder.Models.Responses;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Globalization;

namespace FlooringOrder.Data
{
    public class TestOrderRepository : IOrderRepository
    {
        private List<Order> _orderList = new List<Order>();
        private static Dictionary<string, List<Order>> _orderdictionary = new Dictionary<string, List<Order>>
        {
            { "06/06/2013", new List<Order>
            {
            new Order
            {
                OrderNumber = 1,
                CustomerName = "Wise",
                State = "OH",
                TaxRate = 6.25M,
                ProductType = "Wood",
                Area = 100.00M,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M,
                MaterialCost = 515.00M,
                LaborCost = 475.00M,
                Tax = 61.88M,
                Total = 1051.88M
            },
            new Order
            {
                OrderNumber = 2,
                CustomerName = "Clever",
                State = "MI",
                TaxRate = 7.25M,
                ProductType = "Carpet",
                Area = 200.00M,
                CostPerSquareFoot = 6.15M,
                LaborCostPerSquareFoot = 5.75M,
                MaterialCost = 1230.00M,
                LaborCost = 1150.00M,
                Tax = 1472.74M,
                Total = 3852.74M
            }
            }
            },
            { "05/05/2020", new List<Order>
            {
            new Order
            {
                OrderNumber = 3,
                CustomerName = "Test3",
                State = "OH",
                TaxRate = 6.25M,
                ProductType = "Wood",
                Area = 100.00M,
                CostPerSquareFoot = 5.15M,
                LaborCostPerSquareFoot = 4.75M,
                MaterialCost = 515.00M,
                LaborCost = 475.00M,
                Tax = 61.88M,
                Total = 1051.88M
            },
            new Order
            {
                OrderNumber = 4,
                CustomerName = "Test4",
                State = "MI",
                TaxRate = 7.25M,
                ProductType = "Carpet",
                Area = 200.00M,
                CostPerSquareFoot = 6.15M,
                LaborCostPerSquareFoot = 5.75M,
                MaterialCost = 1230.00M,
                LaborCost = 1150.00M,
                Tax = 1472.74M,
                Total = 3852.74M
            }
            }
            },
            { "01/01/2030", new List<Order>{} }
         };

        public OrderDateFileResponse BuildPath(string orderdate)
        {
            DateTime orderdateout;
            DateTimeFormatInfo info = new DateTimeFormatInfo
            {
                ShortDatePattern = @"MM/dd/yyyy"
            };
            OrderDateFileResponse orderdatefile = new OrderDateFileResponse();
            if (DateTime.TryParseExact(orderdate, @"MM/dd/yyyy", info, DateTimeStyles.None, out orderdateout))
            {
                orderdatefile.path = orderdate;
            }
            return orderdatefile;
        }
        public OrderDateFileResponse GetPath(string orderdate)
        {
            OrderDateFileResponse orderdatefile = new OrderDateFileResponse();
            if (_orderdictionary.ContainsKey(orderdate))
            {
                orderdatefile.path = orderdate;
            }
            else
            {
                orderdatefile.path = "";
            }
            return orderdatefile;
        }
        public Order LoadOrder(string orderdatepath, int ordernumber)
        {
            Order order = new Order();
            if (_orderdictionary.ContainsKey(orderdatepath))
            {
                order = _orderdictionary[orderdatepath].FirstOrDefault(o => o.OrderNumber == ordernumber);
            }
            return order;
        }
        public List<Order> LoadOrders(string orderdatepath)
        {
            _orderList.Clear();
            if (_orderdictionary.ContainsKey(orderdatepath))
            {
                _orderList = _orderdictionary[orderdatepath];
            }
            return _orderList;
        }
        public Order AddOrderToExistingOrderDateFile(Order order, string orderdatepath)
        {
            if (_orderdictionary.ContainsKey(orderdatepath))
            {
                _orderdictionary[orderdatepath].Add(order);
            }
            return order;
        }
        public Order AddOrderToNewOrderDateFile(Order order, string orderdatepath)
        {
            _orderdictionary.Add(orderdatepath, new List<Order> { order });
            return order;
        }

        public Order EditOrder(Order order, string orderdatepath)
        {
            if (_orderdictionary.ContainsKey(orderdatepath))
            {
                var index = _orderdictionary[orderdatepath].FindIndex(o => o.OrderNumber == order.OrderNumber);
                _orderdictionary[orderdatepath][index] = order;
            }
            return order;
        }
        public Order RemoveOrder(Order order, string orderdatepath)
        {
            if (_orderdictionary.ContainsKey(orderdatepath))
            {
                var removeorder = _orderdictionary[orderdatepath].Find(o => o.OrderNumber == order.OrderNumber);
                _orderdictionary[orderdatepath].Remove(removeorder);
            }
            return order;
        }
    }
}
