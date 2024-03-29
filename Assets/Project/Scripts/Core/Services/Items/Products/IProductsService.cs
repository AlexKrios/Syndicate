using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductsService
    {
        List<ProductObject> GetAllProducts();
        ProductObject GetProductByKey(ProductId key);
        ProductObject GetProductById(string id);

        List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId);
    }
}