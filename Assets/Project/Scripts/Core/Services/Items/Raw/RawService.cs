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
    public class RawService : IRawService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        private Dictionary<RawId, RawObject> _rawObjects = new();

        public UniTask Initialize()
        {
            var itemsData = _configurations.RawSet.Items;
            _rawObjects = itemsData.ToDictionary(x => x.Key, x => new RawObject(x));

            return UniTask.CompletedTask;
        }

        public void LoadData(ItemDto data)
        {
            var raw = _rawObjects[(RawId)data.Key];
            raw.Count = data.Count;
        }

        public Dictionary<string, ItemDto> CreateRaw()
        {
            foreach (var (key, _) in _rawObjects)
            {
                _rawObjects[key].Count = 50;
            }

            return _rawObjects.ToDictionary(x => x.Key.ToString(), x => x.Value.ToDto());
        }

        public RawObject GetRaw(PartObject part) => GetRaw((RawId)part.Key);
        public RawObject GetRaw(RawId key)
        {
            return _rawObjects.TryGetValue(key, out var productObject)
                ? productObject
                : throw new Exception($"Can't find {nameof(ProductObject)} with key {key}");
        }
    }
}