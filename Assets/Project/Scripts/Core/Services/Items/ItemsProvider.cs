using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Zenject;

namespace Syndicate.Core.Services
{
    [UsedImplicitly]
    public class ItemsProvider : IItemsProvider
    {
        [Inject] private readonly IRawService _rawService;
        [Inject] private readonly IProductsService _productsService;

        public void LoadItemsData(ItemDto item)
        {
            switch (item.Type)
            {
                case ItemType.Raw:
                    _rawService.LoadData(item);
                    break;

                case ItemType.Component:
                    _productsService.LoadData(item);
                    break;

                case ItemType.Product:
                    _productsService.LoadData(item);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ItemBaseObject GetItem(PartObject part) => GetItem(part.Type, part.Key);
        public ItemBaseObject GetItem(ItemType type, string key)
        {
            return type switch
            {
                ItemType.Raw => _rawService.GetRaw((RawId)key),
                ItemType.Component => _productsService.GetProduct((ProductId)key),
                ItemType.Product => _productsService.GetProduct((ProductId)key),
                _ => null
            };
        }

        public ItemBaseObject GetRandomCraftable()
        {
            var values = Enum.GetValues(typeof(ItemType));
            var random = new Random();
            var randomItemGroup = (ItemType)values.GetValue(random.Next(1, values.Length));

            return randomItemGroup switch
            {
                ItemType.Component => _productsService.GetRandomProduct(),
                ItemType.Product => _productsService.GetRandomProduct(),
                _ => null
            };
        }

        public bool IsHaveNeedItems(List<PartObject> parts)
        {
            var isHaveNeedItems = true;
            foreach (var part in parts)
            {
                var item = GetItem(part);
                if (item.Count < part.Count)
                    isHaveNeedItems = false;
            }

            return isHaveNeedItems;
        }

        public List<ItemBaseObject> RemoveItems(ICraftableItem data)
        {
            var sendList = new List<ItemBaseObject>();
            foreach (var part in data.Parts)
            {
                var item = GetItem(part);
                item.Count -= part.Count;

                sendList.Add(item);
            }

            return sendList;
        }
    }
}