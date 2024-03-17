using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ProductsService : IProductsService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IGameService _gameService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();
        private Dictionary<ProductId, ProductObject> ProductObjects => PlayerProfile.Inventory.Products;

        public UniTask Initialize()
        {
            PlayerProfile.Inventory.Products = _configurations.ProductSet.Items
                .ToDictionary(x => x.Key, x => new ProductObject(x));

            return UniTask.CompletedTask;
        }

        public ProductObject GetProduct(ProductId assetId)
        {
            return ProductObjects.TryGetValue(assetId, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with id {assetId}");
        }

        public List<ProductObject> GetAllProducts() => ProductObjects.Values.ToList();

        public List<ProductObject> GetProductsByUnitType(UnitTypeId unitTypeId)
        {
            return ProductObjects
                .Where(x => x.Value.UnitTypeId == unitTypeId)
                .Select(x => x.Value).ToList();
        }
    }
}