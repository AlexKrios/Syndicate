using System;
using Syndicate.Core.Entities;
using UnityEngine;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UnitSet", menuName = "Scriptable/Units/Unit Set", order = 21)]
    public class UnitSetScriptable : ListScriptableObject<UnitScriptable> { }

    [Serializable]
    public class UnitScriptable
    {
        [SerializeField] private string id;
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private SpriteAssetId spriteAssetId;

        public UnitId Id => (UnitId)id;
        public UnitTypeId UnitTypeId => unitTypeId;
        public SpriteAssetId SpriteAssetId => spriteAssetId;
    }
}