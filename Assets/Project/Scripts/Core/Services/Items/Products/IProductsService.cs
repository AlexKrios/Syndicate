using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductsService
    {
        void LoadProductObjectData(ItemDto data);

        List<ProductObject> GetAllProducts();
        ProductObject GetProductByKey(ProductId key);

        List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId);
    }
}