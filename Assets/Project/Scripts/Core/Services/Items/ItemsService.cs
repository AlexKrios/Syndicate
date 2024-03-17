using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Profile;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ItemsService : IItemsProvider, IItemsService
    {
        [Inject] private readonly IGameService _gameService;
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IComponentsService _componentsService;
        [Inject] private readonly IProductsService _productsService;

        private PlayerProfile PlayerProfile => _gameService.GetPlayerProfile();
        private Dictionary<string, GroupData> Group => PlayerProfile.Inventory.GroupsData;
        private Dictionary<string, ItemData> Items => PlayerProfile.Inventory.ItemsData;

        public ItemBaseObject GetItem(ItemType itemType, string itemId)
        {
            return itemType switch
            {
                ItemType.Raw => _rawService.GetRaw((RawId)itemId),
                ItemType.Component => _componentsService.GetComponent((ComponentId)itemId),
                ItemType.Product => _productsService.GetProduct((ProductId)itemId),
                _ => null
            };
        }

        public T GetItem<T>(string itemId) where T : ItemBaseObject
        {
            if (typeof(T) == typeof(RawObject))
                return _rawService.GetRaw((RawId)itemId) as T;

            if (typeof(T) == typeof(ComponentObject))
                return _componentsService.GetComponent((ComponentId)itemId) as T;

            if (typeof(T) == typeof(ProductObject))
                return _productsService.GetProduct((ProductId)itemId) as T;

            return null;
        }

        public GroupData GetGroupData(ItemType itemType, string key)
        {
            var itemObject = GetItem(itemType, key);
            if (Group.TryGetValue(itemObject.Id, out var value))
            {
                return value;
            }

            var itemCountObject = itemObject.ToGroupData();
            Group.Add(itemObject.Id, itemCountObject);
            return itemCountObject;
        }

        public ItemData GetItemData(ItemType itemType, string key)
        {
            var itemObject = GetItem(itemType, key);
            if (Items.TryGetValue(itemObject.Id, out var value))
            {
                return value;
            }

            var itemData = itemObject.ToItemData();
            Items.Add(itemObject.Id, itemData);
            return itemData;
        }
    }
}