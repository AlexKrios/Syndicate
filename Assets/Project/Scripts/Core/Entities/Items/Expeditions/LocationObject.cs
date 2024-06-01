using System.Collections.Generic;
using Syndicate.Core.Configurations;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    public class LocationObject
    {
        public string Key { get; }

        public List<PartObject> Rewards { get; }

        public LocalizedString NameLocale { get; }
        public LocalizedString DescriptionLocale { get; }

        public SpriteAssetId IconAssetId { get; }

        public LocationObject(LocationScriptable data)
        {
            Key = data.Key;

            Rewards = data.Rewards;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;

            IconAssetId = data.IconAssetId;
        }
    }
}