using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class RawObject : ItemObject
    {
        public RawObject(RawScriptable data)
        {
            ItemTypeId = ItemTypeId.Raw;
            id = data.Id;

            NameLocale = data.NameLocale;
            SpriteAssetId = data.SpriteAssetId;
        }
    }
}