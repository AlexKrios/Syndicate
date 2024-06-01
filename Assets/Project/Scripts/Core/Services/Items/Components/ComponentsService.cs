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
    public class ComponentsService : IComponentsService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        private Dictionary<ComponentId, ComponentObject> _componentObjects;

        public UniTask Initialize()
        {
            _componentObjects = _configurations.ComponentSet.Items
                .ToDictionary(x => x.Key, x => new ComponentObject(x));

            return UniTask.CompletedTask;
        }

        public void LoadComponentObjectData(ItemDto data)
        {
            var raw = _componentObjects[(ComponentId)data.Key];
            raw.Count = data.Count;
            raw.Experience = data.Experience;
        }

        public List<ComponentObject> GetAllComponents() => _componentObjects.Values.ToList();

        public ComponentObject GetComponentByKey(ComponentId key)
        {
            return _componentObjects.TryGetValue(key, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ComponentObject)} with key {key}");
        }
    }
}