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

        private Dictionary<ProductId, ProductObject> _productObjects = new();

        public UniTask Initialize()
        {
            _productObjects = _configurations.ProductSet.Items
                .ToDictionary(x => x.Key, x => new ProductObject(x));

            return UniTask.CompletedTask;
        }

        public void LoadProductObjectData(ItemDto data)
        {
            var raw = _productObjects[(ProductId)data.Key];
            raw.Count = data.Count;
            raw.Experience = data.Experience;
        }

        public List<ProductObject> GetAllProducts() => _productObjects.Values.ToList();

        public ProductObject GetProductByKey(ProductId key)
        {
            return _productObjects.TryGetValue(key, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with key {key}");
        }

        public List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId)
        {
            if (unitTypeId == UnitTypeId.All)
                return _productObjects.Values.ToList();

            return _productObjects
                .Where(x => x.Value.UnitTypeId == unitTypeId)
                .Select(x => x.Value).ToList();
        }
    }
}