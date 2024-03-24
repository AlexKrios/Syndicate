using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ProductsService : IProductsService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        private Dictionary<ProductId, ProductObject> _productObjects;

        public UniTask Initialize()
        {
            _productObjects = _configurations.ProductSet.Items
                .ToDictionary(x => x.Key, x => new ProductObject(x));

            return UniTask.CompletedTask;
        }

        public List<ProductObject> GetAllProducts() => _productObjects.Values.ToList();

        public ProductObject GetProduct(ProductId assetId)
        {
            return _productObjects.TryGetValue(assetId, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with id {assetId}");
        }

        public ProductObject GetProductById(string id)
        {
            var productObject = _productObjects.Values.FirstOrDefault(x => x.Id == id);
            return productObject
                   ?? throw new Exception($"Can't find {nameof(ProductObject)} with id {id}");
        }

        public List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId)
        {
            return _productObjects
                .Where(x => x.Value.UnitTypeId == unitTypeId)
                .Select(x => x.Value).ToList();
        }
    }
}