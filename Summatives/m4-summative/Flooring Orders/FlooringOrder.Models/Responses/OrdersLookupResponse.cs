using System.Collections.Generic;

namespace FlooringOrder.Models.Responses
{
    public class OrdersLookupResponse : Response
    {
        public List<Order> Orderlist { get; set; }
    }
}
