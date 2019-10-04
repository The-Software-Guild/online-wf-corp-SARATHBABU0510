using FlooringOrder.Models;
using FlooringOrder.Models.Interfaces;
using FlooringOrder.Models.Responses;
using System;
using System.Linq;

namespace FlooringOrder.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ITaxRepository _taxRepository;
        public OrderManager(IOrderRepository orderRepository, IProductRepository productRepository, ITaxRepository taxRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _taxRepository = taxRepository;
        }
        public OrderLookupResponse LookupOrder(string orderdate, int ordernumber)
        {
            OrderLookupResponse orderlookupresponse = new OrderLookupResponse();
            OrderDateFileResponse orderdatefile = _orderRepository.GetPath(orderdate);
            if (string.IsNullOrEmpty(orderdatefile.path))
            {
                orderlookupresponse.Success = false;
                orderlookupresponse.Message = $"File not found for Order date {orderdate} or some problem. Please contact IT.";
            }
            else
            {
                orderlookupresponse.order = _orderRepository.LoadOrder(orderdatefile.path, ordernumber);
                if (orderlookupresponse.order == null)
                {
                    orderlookupresponse.Success = false;
                    orderlookupresponse.Message = $"No orders with order date {orderdate} and order number {ordernumber}. Please enter another order date and or order number or contact IT!";
                }
                else
                {
                    orderlookupresponse.Success = true;
                    if (orderlookupresponse.order.CustomerName.Contains("|"))
                    {
                        orderlookupresponse.order.CustomerName = orderlookupresponse.order.CustomerName.Replace("|", ",");
                    }
                }
            }
            return orderlookupresponse;
        }
        public OrdersLookupResponse LookupOrders(string orderdate)
        {
            OrdersLookupResponse orderslookupresponse = new OrdersLookupResponse();
            OrderDateFileResponse orderdatefile = _orderRepository.GetPath(orderdate);
            if (string.IsNullOrEmpty(orderdatefile.path))
            {
                orderslookupresponse.Success = false;
                orderslookupresponse.Message = $"File not found for Order date {orderdate} or some problem. Please contact IT.";
            }
            else
            {
                orderslookupresponse.Orderlist = _orderRepository.LoadOrders(orderdatefile.path);
                if (orderslookupresponse.Orderlist.Count > 0)
                {
                    orderslookupresponse.Success = true;
                }
                else
                {
                    orderslookupresponse.Success = false;
                    orderslookupresponse.Message = $"No orders with {orderdate} or problem with the file. Please enter another order date or contact IT!";
                }
            }
            return orderslookupresponse;
        }        

        public ProcessOrderResponse ProcessOrder(Order order, string orderdate, OrderActionType orderactiontype)
        {
            if (orderactiontype == OrderActionType.Add || orderactiontype == OrderActionType.Edit)
            {
                if (order.CustomerName.Contains(","))
                {
                    order.CustomerName = order.CustomerName.Replace(",", "|");
                }
            }            
            ProcessOrderResponse processorderresponse = new ProcessOrderResponse();                       
            OrderDateFileResponse orderdatefile = _orderRepository.GetPath(orderdate);
            if (string.IsNullOrEmpty(orderdatefile.path))
            {
                processorderresponse.Success = false;
                processorderresponse.Message = $"File not found for Order date {orderdate} or some problem. Please contact IT.";
            }
            else
            {
                switch (orderactiontype)
                {
                    case OrderActionType.Add:
                        processorderresponse.order = _orderRepository.AddOrderToExistingOrderDateFile(order, orderdatefile.path);
                        break;
                    case OrderActionType.Edit:
                        processorderresponse.order = _orderRepository.EditOrder(order, orderdatefile.path);
                        break;
                    case OrderActionType.Remove:
                        processorderresponse.order = _orderRepository.RemoveOrder(order, orderdatefile.path);
                        break;
                    default:
                        throw new Exception("Order Action Type is not supported!");
                }
                if (processorderresponse.order == null)
                {
                    processorderresponse.Success = false;
                    processorderresponse.Message = "New order save (to existing order date file) unsuccessful.";
                }
                else
                {
                    processorderresponse.Success = true;
                }
            }

            if (orderactiontype == OrderActionType.Add && string.IsNullOrEmpty(orderdatefile.path))
            {
                OrderDateFileResponse neworderdatefile = _orderRepository.BuildPath(orderdate);
                if(string.IsNullOrEmpty(neworderdatefile.path))
                {
                    processorderresponse.Success = false;
                    processorderresponse.Message = $"Problem creating a new order date file for Order date {orderdate}. Please contact IT.";
                }
                else
                {
                    processorderresponse.order = _orderRepository.AddOrderToNewOrderDateFile(order, neworderdatefile.path);
                    if (processorderresponse.order == null)
                    {
                        processorderresponse.Success = false;
                        processorderresponse.Message = "New order save (to new order date file) unsuccessful.";
                    }
                    else
                    {
                        processorderresponse.Success = true;
                    }
                }                
            }
            return processorderresponse;
        }
        public ProductsLookupResponse LookupProducts()
        {
            ProductsLookupResponse productslookupresponse = new ProductsLookupResponse();
            productslookupresponse.productlist = _productRepository.LoadProducts();
            if (productslookupresponse.productlist.Count > 0)
            {
                productslookupresponse.Success = true;
            }
            else
            {
                productslookupresponse.Success = false;
                productslookupresponse.Message = $"No products or Product File doesn't exist. Please contact IT!";
            }
            return productslookupresponse;
        }

        public TaxesLookupResponse LookupTaxes()
        {
            TaxesLookupResponse taxeslookupresponse = new TaxesLookupResponse();
            taxeslookupresponse.taxlist = _taxRepository.LoadTaxes();
            if (taxeslookupresponse.taxlist.Count > 0)
            {
                taxeslookupresponse.Success = true;
            }
            else
            {
                taxeslookupresponse.Success = false;
                taxeslookupresponse.Message = $"No tax information or Tax File doesn't exist. Please contact IT!";
            }
            return taxeslookupresponse;
        }

        public int Generateordernumber(string orderdate)
        {
            int ordernumber = 0;
            OrdersLookupResponse orderslookupresponse = LookupOrders(orderdate);
            if (orderslookupresponse.Success)
            {
                var orders = orderslookupresponse.Orderlist.OrderByDescending(o => o.OrderNumber).Take(1).ToList();
                ordernumber = orders.First().OrderNumber + 1;
            }
            else
            {
                ordernumber = 1;
            }
            return ordernumber;
        }

        public Order GenerateOrderInfo(int ordernumber, string customername, string state, string producttype, decimal area)
        {
            Order order = new Order();
            Tax tax = _taxRepository.LoadTax(state);
            Product product = _productRepository.LoadProduct(producttype);            
            order.OrderNumber = ordernumber;            
            order.CustomerName = customername;
            order.State = state;
            order.ProductType = producttype;
            order.Area = area;
            order.TaxRate = tax.TaxRate;
            order.CostPerSquareFoot = product.CostPerSquareFoot;
            order.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
            order.MaterialCost = (order.Area * order.CostPerSquareFoot);
            order.LaborCost = (order.Area * order.LaborCostPerSquareFoot);
            order.Tax = ((order.MaterialCost + order.LaborCost) * (order.TaxRate / 100));
            order.Total = (order.MaterialCost + order.LaborCost + order.Tax);
            return order;
        }
    }
}
