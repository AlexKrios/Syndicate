using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductsService
    {
        ProductObject GetProduct(ProductId assetId);

        List<ProductObject> GetAllProducts();

        List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId);
    }
}