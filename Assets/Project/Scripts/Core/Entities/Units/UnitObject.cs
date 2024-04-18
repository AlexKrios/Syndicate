using Syndicate.Core.Configurations;
using UnityEngine;

namespace Syndicate.Core.Entities
{
    public class UnitObject
    {
        public UnitId Key { get; }

        public UnitTypeId UnitTypeId { get; }

        public SpriteAssetId IconId { get; }

        public GameObject Prefab { get; }

        public UnitObject(UnitScriptable data)
        {
            Key = data.Key;

            UnitTypeId = data.UnitTypeId;

            IconId = data.IconId;

            Prefab = data.Prefab;
        }
    }
}