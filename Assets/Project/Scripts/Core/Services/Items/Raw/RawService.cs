using System;
using System.Collections.Generic;
using System.Linq;
using Syndicate.Core.Configurations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    public class RawService : IRawService, IInitializable
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;

        private Dictionary<RawId, RawObject> _rawAssetsIndex = new();

        public void Initialize()
        {
            _rawAssetsIndex = _configurations.RawSet.Items.ToDictionary(x => x.Key, x => new RawObject(x));
        }

        public RawObject GetRaw(RawId assetId)
        {
            return _rawAssetsIndex.TryGetValue(assetId, out var rawObject)
                ? rawObject
                : throw new Exception($"Can't find {nameof(RawObject)} with id {assetId}");
        }

        public List<RawObject> GetAllRaw() => _rawAssetsIndex.Values.ToList();
    }
}