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
    public class UnitsService : IUnitsService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IGameService _gameService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();
        private Dictionary<UnitId, UnitObject> UnitObjects => PlayerProfile.Inventory.Units;

        public UniTask Initialize()
        {
            PlayerProfile.Inventory.Units = _configurations.UnitSet.Items
                .ToDictionary(x => x.Key, x => new UnitObject(x));

            return UniTask.CompletedTask;
        }

        public UnitObject GetUnit(UnitId assetId)
        {
            return UnitObjects.TryGetValue(assetId, out var unitObject)
                ? unitObject
                : throw new Exception($"Can't find {nameof(UnitObject)} with id {assetId}");
        }

        public List<UnitObject> GetAllUnits() => UnitObjects.Values.ToList();
    }
}