using Syndicate.Core.Configurations;

namespace Syndicate.Core.Entities
{
    public class UnitObject
    {
        public UnitId Key { get; set; }

        public UnitTypeId UnitTypeId { get; set; }

        public SpriteAssetId SpriteAssetId { get; set; }

        public UnitObject(UnitScriptable data)
        {
            Key = data.Key;

            UnitTypeId = data.UnitTypeId;

            SpriteAssetId = data.SpriteAssetId;
        }
    }
}