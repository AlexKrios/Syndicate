using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    public interface ICraftableItem
    {
        string Key { get; }
        ItemType ItemType { get; }

        int Count { get; set; }
        int Experience { get; set; }

        ProductGroupId ProductGroupId { get; }
        UnitTypeId UnitTypeId { get; }

        LocalizedString NameLocale { get; }
        LocalizedString DescriptionLocale { get; }

        SpriteAssetId SpriteAssetId { get; }

        RecipeObject Recipe { get; }
    }
}