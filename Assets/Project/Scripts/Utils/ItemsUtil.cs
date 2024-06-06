using System;
using System.Linq;
using JetBrains.Annotations;
using Syndicate.Core.Entities;

namespace Syndicate.Utils
{
    [UsedImplicitly]
    public class ItemsUtil
    {
        public static ItemType GetItemTypeByKey(string key)
        {
            var itemTypeId = key.Split("|");
            return itemTypeId.First() switch
            {
                Constants.RawId => ItemType.Raw,
                Constants.ComponentId => ItemType.Component,
                Constants.ProductId => ItemType.Product,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static int ParseItemKeyToStar(string key)
        {
            var stringStar = key.Split("|");
            return Convert.ToInt32(stringStar.Last());
        }
    }
}