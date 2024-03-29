using System;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UnitTypeSet", menuName = "Scriptable/Units/Unit Type Set", order = 10)]
    public class UnitTypeSetScriptable : ListScriptableObject<UnitTypeScriptable> { }

    [Serializable]
    public class UnitTypeScriptable
    {
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private Color bgColor;
        [SerializeField] private LocalizedString locale;

        public UnitTypeId UnitTypeId => unitTypeId;
        public Color BgColor => bgColor;
        public LocalizedString Locale => locale;
    }
}