using System;
using System.Collections.Generic;
using Syndicate.Core.Configurations;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    public class LocationObject
    {
        public string Key { get; }
        public int Star { get; set; } = 1;

        public List<UnitPosObject> Enemies { get; }
        public List<PartObject> Rewards { get; }
        public int WayTime { get; }

        public LocalizedString NameLocale { get; }
        public LocalizedString DescriptionLocale { get; }

        public SpriteAssetId IconAssetId { get; }

        public int Experience { get; set; }

        public LocationObject(LocationScriptable data)
        {
            Key = data.Key;

            Enemies = data.Enemies;
            Rewards = data.Rewards;
            WayTime = data.WayTime;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;

            IconAssetId = data.IconAssetId;
        }
    }

    [Serializable]
    public class UnitPosObject
    {
        public ExpeditionSlotId slotId;
        public UnitId unitId;
    }
}