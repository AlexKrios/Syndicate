using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UnitTypeSet", menuName = "Scriptable/Unit Type Set", order = 6)]
    public class UnitTypeSetScriptable : ListScriptableObject<UnitTypeScriptable> { }

    [Serializable]
    public class UnitTypeScriptable
    {
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private LocalizedString locale;

        public UnitTypeId UnitTypeId => unitTypeId;
        public LocalizedString Locale => locale;
    }
}