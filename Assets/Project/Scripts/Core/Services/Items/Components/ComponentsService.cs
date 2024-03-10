using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    public class ComponentsService : IComponentsService, IInitializable
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        private Dictionary<ComponentId, ComponentObject> _componentsAssetsIndex = new();

        public void Initialize()
        {
            _componentsAssetsIndex = _configurations.ComponentSet.Items.ToDictionary(x => x.Key, x => new ComponentObject(x));
        }

        public ComponentObject GetComponent(ComponentId assetId)
        {
            return _componentsAssetsIndex.TryGetValue(assetId, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with id {assetId}");
        }

        public List<ComponentObject> GetAllProducts() => _componentsAssetsIndex.Values.ToList();
    }
}