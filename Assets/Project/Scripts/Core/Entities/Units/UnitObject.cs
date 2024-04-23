using System.Collections.Generic;
using Syndicate.Core.Configurations;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Entities
{
    public class UnitObject
    {
        public UnitId Key { get; }

        public UnitTypeId UnitTypeId { get; }

        public SpriteAssetId IconId { get; }

        public LocalizedString NameLocale { get; }
        public LocalizedString DescriptionLocale { get; }

        public List<SpecificationObject> Specifications { get; }

        public int Experience { get; set; }
        public Dictionary<ProductGroupId, string> Outfit { get; set; }

        public GameObject PrefabAlly { get; }
        public GameObject PrefabEnemy { get; }

        public UnitObject(UnitScriptable data)
        {
            Key = data.Key;

            UnitTypeId = data.UnitTypeId;

            IconId = data.IconId;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;

            Specifications = data.Specifications;

            PrefabAlly = data.PrefabAlly;

            PrefabEnemy = data.PrefabEnemy;
        }
    }
}