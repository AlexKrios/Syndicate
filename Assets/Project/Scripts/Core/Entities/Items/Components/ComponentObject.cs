using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class ComponentObject : ItemObject
    {
        public ComponentId Id => (ComponentId)id;

        public ProductGroupId ProductGroupId { get; set; }
        public UnitTypeId UnitTypeId { get; set; }

        public ComponentObject(ComponentScriptable data)
        {
            ItemTypeId = ItemTypeId.Component;
            id = data.Id;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;
            SpriteAssetId = data.SpriteAssetId;

            ProductGroupId = data.ProductGroupId;
            UnitTypeId = data.UnitTypeId;

            Recipe = data.Recipe;
        }
    }
}