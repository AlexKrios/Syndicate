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

        private Dictionary<UnitId, UnitObject> _unitObjects = new();

        public UniTask Initialize()
        {
            _unitObjects = _configurations.UnitSet.Items.ToDictionary(x => x.Key, x => new UnitObject(x));

            return UniTask.CompletedTask;
        }

        public void LoadUnits(UnitsState state)
        {
            foreach (var (key, value) in state.Roster)
            {
                var unitId = new UnitId(key);
                var unit = _unitObjects[unitId];
                unit.Experience = value.Experience;
                unit.Outfit = value.Outfit;

                unit.IsUnlocked = true;
            }
        }

        public List<UnitObject> GetAllUnits() => _unitObjects.Values.ToList();

        public UnitObject GetUnit(UnitId assetId)
        {
            return _unitObjects.TryGetValue(assetId, out var unitObject)
                ? unitObject
                : throw new Exception($"Can't find {nameof(UnitObject)} with id {assetId}");
        }
    }
}