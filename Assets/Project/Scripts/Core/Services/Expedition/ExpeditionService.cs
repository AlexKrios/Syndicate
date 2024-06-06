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
    public class ExpeditionService : IExpeditionService, IService
    {
        [Inject] private readonly ConfigurationsScriptable _configurations;
        [Inject] private readonly IApiService _apiService;

        private Dictionary<LocationId, LocationObject> _locationObjects = new();

        public int Size { get; set; } = 1;

        private Dictionary<Guid, ExpeditionObject> _queue = new();

        public UniTask Initialize()
        {
            _locationObjects = _configurations.LocationsSet.Items
                .ToDictionary(x => x.Key, x => new LocationObject(x));

            return UniTask.CompletedTask;
        }

        public void LoadData(ExpeditionState state)
        {
            Size = state.Size;

            _queue = state.Queue;
        }

        public List<LocationObject> GetAllLocations() => _locationObjects.Values.ToList();

        public LocationObject GetLocation(LocationId key)
        {
            return _locationObjects.TryGetValue(key, out var locationObject)
                ? locationObject
                : throw new Exception($"Can't find {nameof(LocationObject)} with key {key}");
        }

        public int GetFreeCell()
        {
            var freeIndex = 0;
            for (var i = 1; i <= Size; i++)
            {
                var isIndexBusy = _queue.Values.Any(x => x.Index == i);
                if (isIndexBusy) continue;

                freeIndex = i;
                break;
            }

            return freeIndex;
        }

        public List<ExpeditionObject> GetAllExpedition() => _queue.Values.ToList();

        public async UniTask AddExpedition(ExpeditionObject data)
        {
            await _apiService.Request(_apiService.AddExpedition(data), Finish);

            void Finish() => _queue.Add(data.Guid, data);
        }

        public async UniTask RemoveExpedition(Guid id)
        {
            await _apiService.Request(_apiService.RemoveExpedition(id), Finish);

            void Finish() => _queue.Remove(id);
        }
    }
}