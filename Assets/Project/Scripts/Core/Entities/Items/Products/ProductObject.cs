using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class ProductObject : ItemObject
    {
        public ProductId Id => (ProductId)id;

        public ProductGroupId ProductGroupId { get; set; }
        public UnitTypeId UnitTypeId { get; set; }

        public ProductObject(ProductScriptable data)
        {
            ItemTypeId = ItemTypeId.Product;
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