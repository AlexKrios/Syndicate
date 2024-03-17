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
    public class ComponentsService : IComponentsService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IGameService _gameService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();
        private Dictionary<ComponentId, ComponentObject> ComponentObjects => PlayerProfile.Inventory.Components;

        public UniTask Initialize()
        {
            PlayerProfile.Inventory.Components = _configurations.ComponentSet.Items
                .ToDictionary(x => x.Key, x => new ComponentObject(x));

            return UniTask.CompletedTask;
        }

        public ComponentObject GetComponent(ComponentId assetId)
        {
            return ComponentObjects.TryGetValue(assetId, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ComponentObject)} with id {assetId}");
        }

        public List<ComponentObject> GetAllProducts() => ComponentObjects.Values.ToList();
    }
}