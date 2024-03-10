using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    public class ProductsService : IProductsService, IInitializable
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        private Dictionary<ProductId, ProductObject> _productsAssetsIndex = new();

        public void Initialize()
        {
            _productsAssetsIndex = _configurations.ProductSet.Items.ToDictionary(x => x.Key, x => new ProductObject(x));
        }

        public ProductObject GetProduct(ProductId assetId)
        {
            return _productsAssetsIndex.TryGetValue(assetId, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with id {assetId}");
        }

        public List<ProductObject> GetAllProducts() => _productsAssetsIndex.Values.ToList();

        public List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId)
        {
            return _productsAssetsIndex
                .Where(x => x.Value.UnitTypeId == unitTypeId)
                .Select(x => x.Value).ToList();
        }
    }
}