using System.Collections.Generic;
using FlooringOrder.Models.Responses;

namespace FlooringOrder.Models.Interfaces
{
    public interface IOrderRepository
    {        
        OrderDateFileResponse GetPath(string orderdate);
        OrderDateFileResponse BuildPath(string orderdate);
        Order LoadOrder(string orderdatepath, int ordernumber);
        List<Order> LoadOrders(string orderdatepath);
        Order AddOrderToExistingOrderDateFile(Order order, string orderdatepath);
        Order AddOrderToNewOrderDateFile(Order order, string orderdatepath);
        Order EditOrder(Order order, string orderdatepath);
        Order RemoveOrder(Order order, string orderdatepath);
    }
}
