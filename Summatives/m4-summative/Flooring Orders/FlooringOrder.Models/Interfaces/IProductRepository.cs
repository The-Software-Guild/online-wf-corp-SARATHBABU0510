using System.Collections.Generic;

namespace FlooringOrder.Models.Interfaces
{
    public interface IProductRepository
    {
        Product LoadProduct(string ProductType);
        List<Product> LoadProducts();
    }
}
