using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ProductionService : IProductionService
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IApiService _apiService;
        [Inject] private readonly IItemsProvider _itemsProvider;

        private PlayerState PlayerState => _gameService.GetPlayerState();

        public int Level => PlayerState.Production.Level;
        public int Size => PlayerState.Production.Size;
        private Dictionary<Guid, ProductionObject> Queue => PlayerState.Production.Queue;

        public bool IsHaveFreeCell() => Size > Queue.Count;

        public bool IsHaveNeedItems(ICraftableItem data)
        {
            foreach (var part in data.Recipe.Parts)
            {
                var item = _itemsProvider.GetItemByKey(part.Key);
                if (item.Count < part.Count)
                    return false;
            }

            return true;
        }

        public int GetFreeCell()
        {
            var freeIndex = 0;
            for (var i = 1; i <= Size; i++)
            {
                var isIndexBusy = Queue.Values.Any(x => x.Index == i);
                if (isIndexBusy) continue;

                freeIndex = i;
                break;
            }

            return freeIndex;
        }

        public List<ProductionObject> GetAllProduction() => Queue.Values.ToList();

        public async UniTask AddProduction(ProductionObject productionObject)
        {
            var itemsToRemove = _itemsProvider.RemoveItems(productionObject.ItemRef);
            await _apiService.Request(_apiService.AddProduction(productionObject, itemsToRemove), Finish);

            void Finish() => Queue.Add(productionObject.Guid, productionObject);
        }

        public async void CompleteProduction(Guid id, ICraftableItem item)
        {
            await _apiService.Request(_apiService.CompleteProduction(id, item), Finish);

            void Finish() => Queue.Remove(id);
        }

        public async void AddProductionSize()
        {
            await _apiService.Request(_apiService.SetProductionSize(Size), Finish);

            void Finish() => PlayerState.Production.Size++;
        }

        public async void AddProductionLevel()
        {
            await _apiService.Request(_apiService.SetProductionLevel(Level), Finish);

            void Finish() => PlayerState.Production.Level++;
        }
    }
}