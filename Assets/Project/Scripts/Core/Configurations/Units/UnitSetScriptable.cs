using System;
using System.Collections.Generic;
using Syndicate.Core.Entities;
using UnityEngine;
using UnityEngine.Localization;

namespace Syndicate.Core.Configurations
{
    [CreateAssetMenu(fileName = "UnitSet", menuName = "Scriptable/Units/Unit Set", order = 11)]
    public class UnitSetScriptable : ListScriptableObject<UnitScriptable> { }

    [Serializable]
    public class UnitScriptable
    {
        [SerializeField] private string key;
        [SerializeField] private UnitTypeId unitTypeId;
        [SerializeField] private SpriteAssetId iconId;
        [SerializeField] private LocalizedString nameLocale;
        [SerializeField] private LocalizedString descriptionLocale;
        [SerializeField] private List<SpecificationObject> specifications;

        public UnitId Key => (UnitId)key;
        public UnitTypeId UnitTypeId => unitTypeId;
        public SpriteAssetId IconId => iconId;
        public LocalizedString NameLocale => nameLocale;
        public LocalizedString DescriptionLocale => descriptionLocale;
        public List<SpecificationObject> Specifications => specifications;
    }
}