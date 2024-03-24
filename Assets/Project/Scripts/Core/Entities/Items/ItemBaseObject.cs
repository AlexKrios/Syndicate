using System;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public abstract class ItemBaseObject
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public ItemType ItemType { get; set; }

        public LocalizedString NameLocale { get; set; }
        public LocalizedString DescriptionLocale { get; set; }

        public SpriteAssetId SpriteAssetId { get; set; }

        public RecipeObject Recipe { get; set; }

        public GroupData ToGroupData(string id)
        {
            return new GroupData
            {
                ItemType = ItemType,
                Id = id,
                Experience = 0
            };
        }

        public ItemData ToItemData(string id)
        {
            return new ItemData
            {
                ItemType = ItemType,
                Id = id,
                Count = 0
            };
        }
    }
}