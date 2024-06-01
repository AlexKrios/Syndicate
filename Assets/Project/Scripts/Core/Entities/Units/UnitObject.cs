using System.Collections.Generic;
using System.Linq;
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
        public Dictionary<ProductGroupId, string> Outfit { get; set; } = new();

        public GameObject PrefabAlly { get; }
        public GameObject PrefabEnemy { get; }

        public bool IsUnlocked { get; set; }

        public int Attack => Specifications.First(x => x.Type == SpecificationId.Attack).Value;
        public int Health => Specifications.First(x => x.Type == SpecificationId.Health).Value;
        public int Defense => Specifications.First(x => x.Type == SpecificationId.Defense).Value;
        public int Initiative => Specifications.First(x => x.Type == SpecificationId.Initiative).Value;

        public UnitObject(UnitScriptable data)
        {
            Key = data.Key;

            UnitTypeId = data.UnitTypeId;

            IconId = data.SpriteAssetId;

            NameLocale = data.NameLocale;
            DescriptionLocale = data.DescriptionLocale;

            Specifications = data.Specifications;

            PrefabAlly = data.PrefabAlly;

            PrefabEnemy = data.PrefabEnemy;
        }

        public UnitDto ToDto()
        {
            return new UnitDto
            {
                Key = Key,
                Experience = Experience,
                Outfit = Outfit
            };
        }
    }
}