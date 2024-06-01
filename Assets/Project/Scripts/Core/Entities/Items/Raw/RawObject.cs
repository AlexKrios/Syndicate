﻿using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class RawObject : ItemBaseObject
    {
        public RawObject(RawItemScriptable data)
        {
            ItemType = ItemType.Raw;
            Key = data.Key;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;

            SpriteAssetId = data.SpriteAssetId;
        }
    }
}