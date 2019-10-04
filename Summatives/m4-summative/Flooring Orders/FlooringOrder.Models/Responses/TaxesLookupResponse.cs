using System.Collections.Generic;

namespace FlooringOrder.Models.Responses
{
    public class TaxesLookupResponse : Response
    {
        public List<Tax> taxlist { get; set; }
    }

}
