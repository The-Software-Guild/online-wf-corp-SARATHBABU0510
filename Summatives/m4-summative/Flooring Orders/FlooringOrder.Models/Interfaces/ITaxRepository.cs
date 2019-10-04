using System.Collections.Generic;

namespace FlooringOrder.Models.Interfaces
{
    public interface ITaxRepository
    {   
        Tax LoadTax(string StateAbbreviation);
        List<Tax> LoadTaxes();
    }
}
