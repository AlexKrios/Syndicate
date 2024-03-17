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
    public class RawService : IRawService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IGameService _gameService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();
        private Dictionary<RawId, RawObject> RawObjects => PlayerProfile.Inventory.Raw;

        public UniTask Initialize()
        {
            PlayerProfile.Inventory.Raw = _configurations.RawSet.Items
                .ToDictionary(x => x.Key, x => new RawObject(x));

            return UniTask.CompletedTask;
        }

        public RawObject GetRaw(RawId assetId)
        {
            return RawObjects.TryGetValue(assetId, out var rawObject)
                ? rawObject
                : throw new Exception($"Can't find {nameof(RawObject)} with id {assetId}");
        }

        public List<RawObject> GetAllRaw() => RawObjects.Values.ToList();
    }
}