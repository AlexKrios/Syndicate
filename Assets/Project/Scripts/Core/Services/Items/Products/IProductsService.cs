using System.Collections.Generic;
using Syndicate.Core.Entities;

namespace Syndicate.Core.Services
{
    public interface IProductsService
    {
        void LoadData(ItemDto data);

        List<ProductObject> GetAllProducts();
        ProductObject GetProduct(PartObject part);
        ProductObject GetProduct(ProductId key);
        List<ProductObject> GetProductByUnitKey(UnitTypeId unitTypeId);
        ProductObject GetRandomProduct();
    }
}