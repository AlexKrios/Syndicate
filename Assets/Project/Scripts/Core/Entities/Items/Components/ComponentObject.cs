using Newtonsoft.Json;
using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class ComponentObject : ItemBaseObject, ICraftableItem
    {
        [JsonIgnore] public ProductGroupId ProductGroupId { get; }
        [JsonIgnore] public UnitTypeId UnitTypeId { get; }

        [JsonIgnore] public RecipeObject Recipe { get; }

        public int Experience { get; set; }

        public ComponentObject(ComponentScriptable data)
        {
            ItemType = ItemType.Component;
            Key = data.Key;
            Id = data.Id;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;
            SpriteAssetId = data.SpriteAssetId;

            ProductGroupId = data.ProductGroupId;
            UnitTypeId = data.UnitTypeId;

            Recipe = data.Recipe;
        }
    }
}