using JetBrains.Annotations;
using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    [UsedImplicitly]
    public class RawObject : ItemBaseObject
    {
        public RawObject(RawItemScriptable data)
        {
            Key = data.Key;
            Type = ItemType.Raw;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;
            SpriteAssetId = data.SpriteAssetId;
        }
    }
}