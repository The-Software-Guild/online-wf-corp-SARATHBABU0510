using System.Collections.Generic;

namespace FlooringOrder.Models.Responses
{
    public class ProductsLookupResponse : Response
    {
        public List<Product> productlist { get; set; }
    }
}
