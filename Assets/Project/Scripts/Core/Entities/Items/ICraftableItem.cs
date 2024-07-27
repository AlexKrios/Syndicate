using System.Collections.Generic;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    public interface ICraftableItem
    {
        string Key { get; }
        ItemType Type { get; }

        int Count { get; set; }
        //int Experience { get; set; }

        ProductGroupId ProductGroupId { get; }
        UnitTypeId UnitTypeId { get; }

        LocalizedString NameLocale { get; }
        LocalizedString DescriptionLocale { get; }
        SpriteAssetId SpriteAssetId { get; }

        public List<PartObject> Parts { get; }
        public List<SpecificationObject> Specifications { get; }
        public int CraftTime { get; }
        public int CraftExperience { get; set; }
        public int CraftCost { get; }

        ItemDto ToDto();
        Dictionary<string, object> ToDictionary();
    }
}