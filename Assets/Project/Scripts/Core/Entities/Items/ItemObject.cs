using System;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    [Serializable]
    public class ItemObject
    {
        public string id;
        public ItemTypeId ItemTypeId { get; set; }

        public LocalizedString NameLocale { get; set; }
        public LocalizedString DescriptionLocale { get; set; }
        public SpriteAssetId SpriteAssetId { get; set; }

        public RecipeObject Recipe { get; set; }

        public int Count { get; set; }
    }
}