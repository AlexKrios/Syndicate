using System;
using System.Collections.Generic;
using System.Linq;
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
        [Inject] private readonly IItemsService _itemsService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();

        public int Level => PlayerProfile.Production.Level;
        public int Size => PlayerProfile.Production.Size;
        private Dictionary<Guid, ProductionObject> Queue => PlayerProfile.Production.Queue;

        public bool IsHaveFreeCell() => Size > Queue.Count;

        public bool IsHaveNeedItems(ICraftableItem data)
        {
            foreach (var part in data.Recipe.Parts)
            {
                var item = _itemsService.GetItemData(part.ItemType, part.Key);
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

        public async void AddProduction(ProductionObject productionObject)
        {
            var itemsToRemove = _itemsService.RemoveItems(productionObject.ItemRef);
            Queue.Add(productionObject.Id, productionObject);
            await _apiService.AddProduction(productionObject, itemsToRemove);
        }

        public async void CompleteProduction(Guid id, ItemData itemData, GroupData groupData)
        {
            Queue.Remove(id);
            await _apiService.CompleteProduction(id, itemData, groupData);
        }

        public async void AddProductionSize()
        {
            PlayerProfile.Production.Size++;
            await _apiService.SetProductionSize(Size);
        }

        public async void AddProductionLevel()
        {
            PlayerProfile.Production.Level++;
            await _apiService.SetProductionLevel(Level);
        }
    }
}