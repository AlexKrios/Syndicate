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

        private Dictionary<RawItemId, RawObject> _rawObjects;

        public UniTask Initialize()
        {
            _rawObjects = _configurations.RawSet.Items
                .ToDictionary(x => x.Key, x => new RawObject(x));

            return UniTask.CompletedTask;
        }

        public RawObject GetRawByKey(RawItemId key)
        {
            return _rawObjects.TryGetValue(key, out var rawObject)
                ? rawObject
                : throw new Exception($"Can't find {nameof(RawObject)} with key {key}");
        }

        public RawObject GetRawById(string id)
        {
            var componentObject = _rawObjects.Values.FirstOrDefault(x => x.Id == id);
            return componentObject
                   ?? throw new Exception($"Can't find {nameof(RawObject)} with id {id}");
        }

        public List<RawObject> GetAllRaw() => _rawObjects.Values.ToList();
    }
}