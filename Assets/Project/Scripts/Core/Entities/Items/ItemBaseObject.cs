using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public abstract class ItemBaseObject
    {
        public string Key { get; set; }
        public ItemType Type { get; set; }

        public LocalizedString NameLocale { get; set; }
        public LocalizedString DescriptionLocale { get; set; }
        public SpriteAssetId SpriteAssetId { get; set; }

        public int Count { get; set; }

        public virtual ItemDto ToDto()
        {
            return new ItemDto
            {
                Key = Key,
                Type = Type,
                Count = Count
            };
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Key"] = Key,
                ["Type"] = Type.ToString(),
                ["Count"] = Count
            };
        }
    }
}