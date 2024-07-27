using System.Collections.Generic;
using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class ProductObject : ItemBaseObject, ICraftableItem
    {
        public ProductGroupId ProductGroupId { get; }
        public UnitTypeId UnitTypeId { get; }

        public List<PartObject> Parts { get; }
        public List<SpecificationObject> Specifications { get; }
        public int CraftTime { get; }
        public int CraftExperience { get; set; }
        public int CraftCost { get; }

        public ProductObject(ProductScriptable data)
        {
            Key = data.Key;
            Type = data.Type;

            ProductGroupId = data.ProductGroupId;
            UnitTypeId = data.UnitTypeId;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;
            SpriteAssetId = data.SpriteAssetId;

            var recipe = data.Recipe;
            Parts = recipe.Parts;
            Specifications = recipe.Specifications;
            CraftTime = recipe.CraftTime;
            CraftExperience = recipe.Experience;
            CraftCost = recipe.Cost;
        }
    }
}