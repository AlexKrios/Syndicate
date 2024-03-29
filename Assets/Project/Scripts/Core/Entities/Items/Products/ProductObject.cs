﻿using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class ProductObject : ItemBaseObject, ICraftableItem
    {
        public ProductGroupId ProductGroupId { get; }
        public UnitTypeId UnitTypeId { get; }

        public int Experience { get; set; }

        public ProductObject(ProductScriptable data)
        {
            ItemType = ItemType.Product;
            Id = data.Id;
            Key = data.Key;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;
            SpriteAssetId = data.SpriteAssetId;

            ProductGroupId = data.ProductGroupId;
            UnitTypeId = data.UnitTypeId;

            Recipe = data.Recipe;
        }
    }
}