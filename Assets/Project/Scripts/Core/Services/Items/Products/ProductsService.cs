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
            var itemsData = _configurations.ProductSet.Items;
            _productObjects = itemsData.ToDictionary(x => x.Key, x => new ProductObject(x));

            return UniTask.CompletedTask;
        }

        public void LoadData(ItemDto data)
        {
            var product = GetProduct((ProductId)data.Key);
            product.Count = data.Count;
        }

        public List<ProductObject> GetAllProducts() => _productObjects.Values.ToList();

        public ProductObject GetProduct(PartObject part) => GetProduct((ProductId)part.Key);
        public ProductObject GetProduct(ProductId key)
        {
            return _productObjects.TryGetValue(key, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with key {key}");
        }

        public List<ProductObject> GetProductByUnitKey(UnitTypeId unitTypeId)
        {
            return unitTypeId == UnitTypeId.All
                ? GetAllProducts()
                : _productObjects.Values.Where(x => x.UnitTypeId == unitTypeId).ToList();
        }

        public ProductObject GetRandomProduct()
        {
            var random = new Random();
            return _productObjects.Values.ElementAt(random.Next(0, _productObjects.Count));
        }
    }
}