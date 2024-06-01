using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public abstract class ItemBaseObject
    {
        public string Key { get; set; }
        public ItemType ItemType { get; set; }

        public int Count { get; set; }
        public int Experience { get; set; }

        public LocalizedString NameLocale { get; set; }
        public LocalizedString DescriptionLocale { get; set; }

        public SpriteAssetId SpriteAssetId { get; set; }

        public RecipeObject Recipe { get; set; }

        public ItemDto ToDto()
        {
            return new ItemDto
            {
                Key = Key,
                Count = Count,
                Experience = Experience,
            };
        }

        public Dictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Key"] = Key,
                ["Count"] = Count,
                ["Experience"] = Experience
            };
        }
    }
}