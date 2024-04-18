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
        [SerializeField] private string key;
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private SpriteAssetId iconId;
        [SerializeField] private GameObject prefab;

        public UnitId Key => (UnitId)key;
        public UnitTypeId UnitTypeId => unitTypeId;
        public SpriteAssetId IconId => iconId;
        public GameObject Prefab => prefab;
    }
}