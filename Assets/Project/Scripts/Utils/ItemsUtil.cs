using System;
using System.Linq;
using JetBrains.Annotations;
using Syndicate.Core.Entities;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class ItemsUtil
    {
        public static string[] ParseItemIdToPartIds(string id)
        {
            return id.Split("_");
        }

        public static string ParseItemIdToGroupId(string id)
        {
            return id.Split("_").First();
        }

        public static ItemType GetItemTypeById(string id)
        {
            var itemTypeId = id.Split("|");
            return itemTypeId.First() switch
            {
                Constants.RawId => ItemType.Raw,
                Constants.ComponentId => ItemType.Component,
                Constants.ProductId => ItemType.Product,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static int ParseItemIdToStar(string id)
        {
            var stringStar = id.Split("|");
            return Convert.ToInt32(stringStar[2]);
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
    }
}