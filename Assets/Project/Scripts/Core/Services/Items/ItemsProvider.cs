using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Utils;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ItemsProvider : IItemsProvider
    {
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IComponentsService _componentsService;
        [Inject] private readonly IProductsService _productsService;

        public void LoadItemsData(ItemDto item)
        {
            var itemType = ItemsUtil.GetItemTypeByKey(item.Key);
            switch (itemType)
            {
                case ItemType.Raw:
                    _rawService.LoadRawObjectData(item);
                    break;

                case ItemType.Component:
                    _componentsService.LoadComponentObjectData(item);
                    break;

                case ItemType.Product:
                    _productsService.LoadProductObjectData(item);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ItemBaseObject GetItemByKey(string key)
        {
            var itemType = ItemsUtil.GetItemTypeByKey(key);
            return itemType switch
            {
                ItemType.Raw => _rawService.GetRawByKey((RawItemId)key),
                ItemType.Component => _componentsService.GetComponentByKey((ComponentId)key),
                ItemType.Product => _productsService.GetProductByKey((ProductId)key),
                _ => null
            };
        }

        public ICraftableItem GetCraftableItemByKey(string key)
        {
            var itemType = ItemsUtil.GetItemTypeByKey(key);
            return itemType switch
            {
                ItemType.Component => _componentsService.GetComponentByKey((ComponentId)key),
                ItemType.Product => _productsService.GetProductByKey((ProductId)key),
                _ => null
            };
        }

        public List<ItemBaseObject> RemoveItems(ICraftableItem data)
        {
            var sendList = new List<ItemBaseObject>();
            foreach (var part in data.Recipe.Parts)
            {
                var item = GetItemByKey(part.Key);
                item.Count -= part.Count;

                sendList.Add(item);
            }

            return sendList;
        }
    }
}