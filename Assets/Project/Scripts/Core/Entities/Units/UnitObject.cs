using Syndicate.Core.Configurations;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    public class UnitObject
    {
        public UnitId Key { get; }

        public UnitTypeId UnitTypeId { get; }

        public SpriteAssetId IconId { get; }

        public GameObject PrefabAlly { get; }
        public GameObject PrefabEnemy { get; }

        public UnitObject(UnitScriptable data)
        {
            Key = data.Key;

            UnitTypeId = data.UnitTypeId;

            IconId = data.IconId;

            PrefabAlly = data.PrefabAlly;
            
            PrefabEnemy = data.PrefabEnemy;
        }
    }
}