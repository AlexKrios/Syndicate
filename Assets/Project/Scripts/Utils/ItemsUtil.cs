using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Syndicate.Core.Entities;
using Syndicate.Core.Services;
using Zenject;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class ItemsUtil
    {
        [Inject] private readonly IItemsProvider _itemsProvider;

        public static string[] ParseItemToIds(string itemId)
        {
            return itemId.Split("_");
        }

        public static int ParseItemToStar(string itemId)
        {
            var stringStar = itemId.Split("|");
            return Convert.ToInt32(stringStar[1]);
        }

        public static List<PartData> ParseItemToParts(ItemBaseObject itemData)
        {
            var parts = new List<PartData>();
            var itemParts = itemData.Recipe.Parts;
            itemParts.ForEach(x => parts.Add(new PartData
            {
                ItemType = x.ItemType,
                Key = x.Key
            }));

            return parts;
        }

        public static string ParseItemToId(ItemBaseObject itemData)
        {
            var id = itemData.Id;
            var recipe = itemData.Recipe;
            if (recipe == null || recipe.Parts.Count == 0)
                return id;

            foreach (var part in recipe.Parts)
            {
                id = $"{id}_{part.Id}";
            }

            return id;
        }

        public ItemBaseObject ParseItemIdToItem(ItemData itemData)
        {
            var stringParts = itemData.Id.Split("_");
            return _itemsProvider.GetItemById(itemData.ItemType, stringParts[0]);
        }
    }
}