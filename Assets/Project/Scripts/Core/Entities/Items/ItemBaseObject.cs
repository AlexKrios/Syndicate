using System;
using Newtonsoft.Json;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public abstract class ItemBaseObject
    {
        [JsonIgnore] public string Key { get; set; }
        public string Id { get; set; }
        [JsonIgnore] public ItemType ItemType { get; set; }

        [JsonIgnore] public LocalizedString NameLocale { get; set; }
        [JsonIgnore] public LocalizedString DescriptionLocale { get; set; }

        [JsonIgnore] public SpriteAssetId SpriteAssetId { get; set; }

        public GroupData ToGroupData()
        {
            return new GroupData
            {
                ItemType = ItemType,
                Id = Id,
                Experience = 0
            };
        }

        public ItemData ToItemData()
        {
            return new ItemData
            {
                ItemType = ItemType,
                Id = Id,
                Count = 0
            };
        }
    }
}